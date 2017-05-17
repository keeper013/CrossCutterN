# CrossCutterN: A light weight AOP tool for .NET

_CrossCutterN_ is designed to allow .NET developers to inject customized AOP code into existing assemblies without the need of re-compilation of them. It allows to inject customized AOP code at the point of **entry**, **exception** and **exit** of **methods** and **property getters and setters** via **custom attributes** and **name matching**.

## Quick Examples

:exclamation: In the examples, this article assumes that readers have cloned this project into there own environment and are able to compile and run the project binaries.

### AOP via Custom Attribute

For projects under development, AOP via custom attribute is a good way to inject customized AOP codes.

The following C# code (The code piece can be found in _CrossCutterN.SampleTarget_ project):

```C#
class Target
{
    [SampleConcernMethod]
    public static int Add1(int x, int y)
    {
    	Console.Out.WriteLine("Inside Add1, starting");
    	var z = x + y;
        Console.Out.WriteLine("Inside Add1, ending");
        return z;
    }
}

class Program
{
    static void Main(string[] args)
    {
    	Target.Add1(1, 2);
    }
}
```

Outputs:

```
Inside Add1, starting
Inside Add1, ending
```

To inject the following AOP code into Add1 method (The code piece can be found in _CrossCutterN.SampleAdvice_ project):

```C#
public sealed class SampleConcernMethodAttribute : MethodConcernAttribute
{
}
    
public static class Advices
{
    public static void OnEntry(IExecution execution)
    {
    	var strb = new StringBuilder(execution.Name);
        strb.Append("(");
        if (execution.Parameters.Count > 0)
        {
            foreach (var parameter in execution.Parameters)
            {
            	strb.Append(parameter.Name).Append("=").Append(parameter.Value).Append(",");
            }
            strb.Remove(strb.Length - 1, 1);
        }
        strb.Append(")");
        Console.Out.WriteLine("Entry at {0}: {1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"), strb.ToString());
    }

    public static void OnExit(IReturn rReturn)
    {
    	if (rReturn.HasReturn)
    	{
            Console.Out.WriteLine("Exit at {0}: returns {1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"), rReturn.Value);
        }
        else
        {
            Console.Out.WriteLine("Exit at {0}: no return", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"));
        }
    }
}
```
Now if the reader follow the steps below:
1. Compile the whole project (Here we assume Debug configuration is used).
2. Copy _CrossCutterN.SampleAdvice/bin/Debug/CrossCutterN.SampleAdvice.dll_ to _CrossCutterN.Command/bin/Debug/_.
3. Copy _CrossCutterN.SampleTarget/CrossCutterN.Command.exe.config_ to _CrossCutterN.Command/bin/Debug_, overwrite existing file with the same name, the relevant content in the configuration file for this injection is as below:

```xml
<concernAttributeAspectBuilders>
    <add id="Builder1">
        <factoryMethod type="CrossCutterN.Aspect.Builder.AspectBuilderFactory, CrossCutterN.Aspect"
                       method="InitializeConcernAttributeAspectBuilder"
                       methodConcernAttribute="CrossCutterN.SampleAdvice.SampleConcernMethodAttribute, CrossCutterN.SampleAdvice"/>
        <pointcut>
            <add joinPoint="Entry" sequence="1"
		 classType="CrossCutterN.SampleAdvice.Advices, CrossCutterN.SampleAdvice" method="OnEntry" parameterPattern="Execution"/>
	    <add joinPoint="Exit" sequence="1"
                 classType="CrossCutterN.SampleAdvice.Advices, CrossCutterN.SampleAdvice" method="OnExit" parameterPattern="Return"/>
	</pointcut>
    </add>
</concernAttributeAspectBuilders>
```
4. In Windows Command Prompt, navigate to _CrossCutterN.Command/bin/Debug/_ directory, and execute the following command to load _CrossCutterN.SampleTarget.exe_ assembly, inject it with AOP code above, and output a new assembly named _CrossCutterN.SampleTarget_Weaved.exe_:

```batchfile
CrossCutterN.Command.exe ../../../CrossCutterN.SampleTarget/bin/Debug/CrossCutterN.SampleTarget.exe ../../../CrossCutterN.SampleTarget/bin/Debug/CrossCutterN.SampleTarget_Weaved.exe
```
5. The execution should be successful, a log file of weaving statistics should be generated under _CrossCutterN.Command/bin/Debug/ directory_.
6. Nevigate to _CrossCutterN.SampleTarget/bin/Debug/_ directory, _CrossCutterN.SampleTarget_Weaved.exe_ should be generated there already.
7. Execute _CrossCutterN.SampleTarget_Weaved.exe_, something like the following result suggests that OnEntry and OnExit methods have been injected into _CrossCutterN.SampleTarget_ assembly.
```
Entry at 2017-05-16 07:51:24.332 PM: Add1(x=1,y=2)
Inside Add1, starting
Inside Add1, ending
Exit at 2017-05-16 07:51:24.332 PM: returns 3
```
In the above example, the configuration section tells _CrossCutter.Command.exe_ tool to inject OnEntry and OnExit methods into all methods that has custom attribute _CrossCutterN.SampleAdvice.SampleConcernMethodAttribute_.

### AOP via Name Matching

For assemblies that need AOP injections but can't be re-compiled for certain reasons (like source code unavailable or releasing activity for new versions is limited), this is an effective way to inject AOP codes into these assemblies without the need of re-compilation.

The steps for AOP injection are about the same with the former example, only a little differences of the code and configuration.  

_CrossCutterN.SampleTarget_ code:

```C#
class Target
{
    public static int Add2(int x, int y)
    {
    	Console.Out.WriteLine("Inside Add1, starting");
    	var z = x + y;
        Console.Out.WriteLine("Inside Add1, ending");
        return z;
    }
}

class Program
{
    static void Main(string[] args)
    {
    	Target.Add2(1, 2);
    }
}
```

Relevent element in configuration file:

```xml
<nameExpressionAspectBuilders>
    <add id="Builder2">
	<factoryMethod type="CrossCutterN.Aspect.Builder.AspectBuilderFactory, CrossCutterN.Aspect"
                       method="InitializeNameExpressionAspectBuilder">
	    <includes>
		<add expression="CrossCutterN.SampleTarget.Target.Add2"/>
	    </includes>
	</factoryMethod>
	<pointcut>
	    <add joinPoint="Entry" sequence="2"
		 classType="CrossCutterN.SampleAdvice.Advices, CrossCutterN.SampleAdvice" method="OnEntry" parameterPattern="Execution"/>
	    <add joinPoint="Exit" sequence="2"
		 classType="CrossCutterN.SampleAdvice.Advices, CrossCutterN.SampleAdvice" method="OnExit" parameterPattern="Return"/>
	</pointcut>
    </add>
</nameExpressionAspectBuilders>
```

And the result is something like the following when executing _CrossCutterN.SampleTarget_Weaved.exe_:

```
Entry at 2017-05-16 09:13:15.218 PM: Add2(x=1,y=2)
Inside Add1, starting
Inside Add1, ending
Exit at 2017-05-16 09:13:15.218 PM: returns 3
```

The configuration tells _CrossCutterN.Command.exe_ tool to inject OnEntry and OnExit methods into all methods that match name "CrossCutterN.SampleTarget.Target.Add2" (For a name without wildcard, of course there can be at most 1 matching, wildcard for name pattern matching will be talked about below).

## Feature Summary

_CrossCutterN.Command.exe_ tool is highly flexible and configurable. For more complete usage examples, please refer to content in _CrossCutterN.Test_ project.

### Join Point

_CrossCutterN_ allows to inject AOP code at 3 points of methods/property getters and setters:

* Entry
* Exception
* Exit

:exclamation: The AOP functions to be injected into assemblies must be static functions with "void" as return type (don't see any point to instantiate any dummy objects or return anything for injectable methods). For each join point, the injectable methods' parameter list must follow certain patterns, which are enforced by validation during injection process, as listed below:

#### Entry

| Parameter List Pattern | Configuration Value |
| --- | --- |
| () | Empty |
| (CrossCutterN.Advice.Parameter.IExecution) | Execution |

#### Exception

| Parameter List Pattern | Configuration Value |
| --- | --- |
| () | Empty |
| (CrossCutterN.Advice.Parameter.IExecution) | Execution |
| (CrossCutterN.Advice.Parameter.IExecution, System.Exception) | ExecutionException |

#### Exit

| Parameter List Pattern | Configuration Value |
| --- | --- |
| () | Empty |
| (CrossCutterN.Advice.Parameter.IExecution) | Execution |
| (CrossCutterN.Advice.Parameter.IReturn) | Return |
| (bool) | HasException |
| (CrossCutterN.Advice.Parameter.IExecution, CrossCutterN.Advice.Parameter.IReturn) | ExecutionReturn |
| (CrossCutterN.Advice.Parameter.IExecution, bool) | ExecutionHasException |
| (CrossCutterN.Advice.Parameter.IReturn, bool) | ReturnHasException |
| (CrossCutterN.Advice.Parameter.IExecution, CrossCutterN.Advice.Parameter.IReturn, bool) | ExecutionReturnHasException |

* _CrossCutterN.Advice.Parameter.IExecution_ parameter contains information of the injected method/property, which includes assembly name, module name, class name, method name, parameter list (which includes name, type and value for each parameter)
* _System.Exception_ parameter for exception join point is the uncaught exception thrown during execution of the method/property injected  
* _CrossCutterN.Advice.Parameter.IReturn_ parameter contains return value information of the execution, which includes if has return value (in case of exception thrown uncaught or return type if "void" there will be no return value), return value type and return value itself.
* _bool_ parameter type for exit join point indicates whether an exception has been thrown and uncaught at the point of leaving the injected method/property. This is an easy way to know whether the execution is successful at the exit point of the method/property. 

In configuration file, when defining methods to be injected into join points (will be referred to as **_advice_** s in the following content), the _parameterPattern_ configuration attribute should be set to the corresponding configuration value in the tables above to specify parameter list pattern of the advice method.

#### Example for Demonstration

Let's take an example to see what happens after an assembly is injected.

If before injection a method looks like the following:

```C#
namespace CrossCutterN.SampleTarget
{
    using System;
    using System.Text;

    class Sample
    {
        public static string SampleMethod(int x, StringBuilder strb)
        {
            if (strb != null)
            {
                return strb.ToString() + x;
            }
            else
            {
                return x.ToString();
            }
        }
    }
}
```

After injection, the following C# code implementation represents the IL in the injected method:

```C#
namespace CrossCutterN.SampleTarget
{
    using System;
    using System.Text;

    class Sample
    {
        public static string SampleMethod(int x, StringBuilder strb)
        {
	    // The following local variables will only be initialized if needed for injected advices
            string returnValue = null;
            var executionContext = CrossCutterN.Advice.Parameter.ParameterFactory.InitializeExecutionContext();
            var executionParameter = CrossCutterN.Advice.Parameter.ParameterFactory.InitializeExecution(
                "CrossCutterN.SampleTarget, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", 
                "CrossCutterN.SampleTarget", 
                "CrossCutterN.SampleTarget.Sample", 
                "Sample", 
                "System.String CrossCutterN.SampleTarget.Sample::SampleMethod(System.String,System.Text.StringBuilder)", 
                "SampleMethod", 
                "System.String");
            executionParameter.AddParameter(
                CrossCutterN.Advice.Parameter.ParameterFactory.InitializeParameter("x", "System.Int32", 0, x).ToReadOnly());
            executionParameter.AddParameter(
                CrossCutterN.Advice.Parameter.ParameterFactory.InitializeParameter("strb", "System.Text.StringBuilder", 1, strb).ToReadOnly());
            var execution = executionParameter.ToReadOnly();
            var returnParameter = CrossCutterN.Advice.Parameter.ParameterFactory.InitializeReturn(true, "System.String");
            try
            {
                // Injected Entry Advices
                EntryAdvice1(execution);
                EntryAdvice2();
                ...
                if (strb != null)
                {
                    returnValue = strb.ToString() + x;
                }
                else
                {
                    returnValue = x.ToString();
                }
                return returnValue;
            }
            catch (Exception e)
            {
                executionContext.MarkExceptionThrown();
                returnParameter.HasReturn = false;
                // Injected Exception Advices
                ExceptionAdvice1(execution);
                ExceptionAdvice2(execution, e);
                ExceptionAdvice3(e);
                ...
            }
            finally
            {
                if (!executionContext.ExceptionThrown)
                {
                    returnParameter.Value = returnValue;
                }
                var rtn = returnParameter.ToReadOnly();
                // Injected Exit Advices
                ExitAdvice1(execution);
                ExitAdvice2(rtn);
                ExitAdvice3(executionContext.ExceptionThrown);
                ExitAdvice4(execution, rtn);
                ExitAdvice5(execution, executionContext.ExceptionThrown);
                ExitAdvice6(rtn, executionContext.ExceptionThrown);
                ExitAdvice7(execution, rtn, executionContext.ExceptionThrown);
                ...
            }
        }
    }
}
```
Only the injected portion is done by IL weaving instead of written in C# code. After a successful injection, all a developer needs to do is to copy CrossCutterN.Advice assembly and the assembly that contains the declaration and implementation of injected methods (like EntryAdvice1, ExitAdvice7 in the represented code above) in to the search path of the running program (most conveniently the same folder with the injected assembly), to make sure necessary assemblies can be found by the program.

### Multiple Injections to the Same Method/Property 

Considering in real life development, multiple concerns may apply to the same method/property (logging, validation, authorization and so on), _CrossCutterN_ is designed to allow such use cases. Injection for one concern is described as one **_AspectBuilder_**, which can generate one **_Aspect_** for a method/property to be injected. One **_Aspect_** may contain one **_advice_** for each join point listed above. When injecting to one target method/property, all **_AspectBuilder_** s will sequentially generate **_Aspect_** s for the target, then each **_advice_** in the **_Aspect_** s will be summarized for each join point and injected to the join point of the target sequentially according to the sequence number applied to the **_AspectBuilder_** which the **_advice_** comes from.  
In configuration file, sequence number must be applied to each join point configuration element. For the same join point, sequence number of each **_AspectBuilder_** must be different, which is enforced by validation during injection process.

### Conveniont Injection Control

To allow developers to easily include most methods/properties and exclude certain methods/properties in a class, AOP code injection via custom attribute is designed to be overwritable by hierachy.

Four pre-defined attribute types are provided:

**_CrossCutterN.Concern.Attribute.ClassConcernAttribute_**: this attribute is valid for classes that all methods and properties are subjects for AOP injection within the classes which has this attribute. It contains following properties

| Property | Description | Default Value |
| --- | --- | --- |
| ConcernPublic | if set to false, all public methods/properties in the class won't be injected | true |
| ConcernProtected | if set to false, all protected methods/properties in the class won't be injected | false |
| ConcernInternal | if set to false, all internal methods/properties in the class won't be injected | false |
| ConcernPrivate | if set to false, all private methods/properties in the class won't be injected | false |
| ConcernInstance | if set to false, all instance methods/properties in the class won't be injected | true |
| ConcernStatic | if set to false, all static methods/properties in the class won't be injected | true |
| ConcernMethod | if set to false, all static methods in the class won't be injected | true |
| ConcernPropertyGetter | if set to false, all property getters in the class won't be injected | false |
| ConcernPropertySetter | if set to false, all property setters in the class won't be injected | false |
| PointCutAtEntry | if set to false, injection to Entry join point will be disabled | true |
| PointCutAtException | if set to false, injection to Exception join point will be disabled | true |
| PointCutAtExit | if set to false, injection to Exit join point will be disabled | true |

**_CrossCutterN.Concern.Attribute.MethodConcernAttribute_**: this attribute is valid for methods and propery getters/setters. The mentioned items will be injected if having this custom attribute, regargless if ConcernMethod/ConcernPropertyGetter/ConcernPropertySetter attribute properties are set to false in the class concern attribute, or if there is no class concern attribute applied to the class. It contains following properties:

| Property | Description | Default Value |
| --- | --- | --- |
| PointCutAtEntry | if set to false, injection to Entry join point will be disabled | true |
| PointCutAtException | if set to false, injection to Exception join point will be disabled | true |
| PointCutAtExit | if set to false, injection to Exit join point will be disabled | true |

These attribute properties will overwrite the attribute properties with the same name in concern class attribute or concern property value.

**_CrossCutterN.Concern.Attribute.PropertyConcernAttribute_**: this attribute is valid for properties only. The property getters/setters will be injected if the properties have this attribute, regardless if \ConcernPropertyGetter/ConcernPropertySetter attribute properties are set to false in the class concern attribute, or if there is no class concern attribute applied to the class. It contains following properties:

| Property | Description | Default Value |
| --- | --- | --- |
| ConcernPropertyGetter | if set to false, getter of the property won't be injected | false |
| ConcernPropertySetter | if set to false, setters of the property won't be injected | false |
| PointCutAtEntry | if set to false, injection to Entry join point will be disabled | true |
| PointCutAtException | if set to false, injection to Exception join point will be disabled | true |
| PointCutAtExit | if set to false, injection to Exit join point will be disabled | true |

These attribute properties will overwrite the attribute properties with the same name in concern class attribute.

**_CrossCutterN.Concern.Attribute.NoConcernAttribute_**: this attribute is valid for methods/properties/property getters/property setters. The mentioned items and their child items (methods to class, getter/setter to property) will not be injected if having this attribute, unless overwritten. It has no properties.

:exclamation: The pre-defined attributes are all declared as abstract to force developers to inherite from them and use their own custom attributes for AOP code injection. The reason for this is to avoid re-using of the same existing attribute for multiple concern injections. It is encouraged to have dedicated set of attributes for different concern's injection.  
:exclamation: For the sake of keeping pre-defined attribute properties for injection logic, custom implementation of these 4 attributes must inherit from the above properties. This is enforced by validation of parent class type during injection process.  
:exclamation: Developers are not requested to inherit from all 4 attributes above to build a complete set of attributes. For example, if only few methods needs to be injected, only inheriting **_CrossCutterN.Concern.Attribute.MethodConcernAttribute_** is good enough. However, at least on of **_CrossCutterN.Concern.Attribute.ClassConcernAttribute_**, **_CrossCutterN.Concern.Attribute.MethodConcernAttribute_** and **_CrossCutterN.Concern.Attribute.PropertyConcernAttribute_** should be inherited to pass the validation of **_ConcernAttributeAspectBuilder_**.  
:exclamation: Note that in configuration section, **_ConcernAttributeAspectBuilder_** and **_NameExpressionAspectBuilder_** have similar configuration settings as the attribute properties listed above. They are **not** the same. The configuration settings in the configuration file are default settings of **_AspectBuilder_** s, hard coded attribute properties, if exist, in those attributes above in the source code overwrites the default settings of the **_AspectBuilder_** s.
 
 ### AOP Code Injection via Name Matching
 
AOP code injection via name matching supports including and excluding methpd/property name patterns. If a method's/properties' name matches an excluded pattern, it won't be injected even if it also matches one of the included patterns.
 
Including/excluding patterns allow the usage of asterisk character ("\*" character) as a wildcard to match any number of any characters, and this is the only supported wildcard character for pattern matching for now.

## Project Structure
* **_CrossCutterN.Advice_**: Basic support assembly for _CrossCutterN_ tool. Please make sure that this assembly is copied over to the directories where injected assemblies are deployed. It contains pre-defined advice parameters.
* **_CrossCutterN.Aspect_**: Contains the definition of the 2 ways of AOP code injection. _CrossCutterN.Command_ tool depends on this assembly. Also, to have customized ways for AOP code injection, this assembly is also the extension point.
* **_CrossCutterN.Weaver_**: the module that does the code injection using _Mono.Cecil_ IL weaving technologies. _CrossCutterN.Command_ tool depends on this assembly, and this assembly is not a designed extension point.
* **_CrossCutterN.Command_**: the executable assembly of CrossCutterN tool, which loads assemblies and output injected assemblies. The configuration definition is included in it.
* **_CrossCutterN.Concern_**: The base classes of the custom attribute used for AOP code injection are defined in this module. The base classes are defined as abstract classes to force developers to extend these attribute to have custom attributes for AOP code injection. The idea is developers are encouraged to use dedicated attributes for each AOP code injection scheme, so re-using of existing attributes is disabled to avoid "lazy" extends.
* **_CrossCutterN.Test_**: Unit test project for CrossCutterN. Please not that in post build event, _CrossCutterN.Command.exe_ and it's dependencies will be copied over to output folder of this project, execute the tool to generate a injected dll, and the injected dll will replace the original one, so test cases can be executed immediately after rebuild of this project. Post build event also clean up _CrossCutterN.Command_ and it's dependencies from this projects output directory after injection is done.
* **_CrossCutterN.SampleAdvice_**: Sample AOP code for a quick demonstration of the tool.
* **_CrossCutterN.SampleTarget_**: Sample assembly to be injected for a quick demonstration of the tool.

## References

Please refer to [AOP Wikipedia](https://en.wikipedia.org/wiki/Aspect-oriented_programming) for background knowledge of AOP.

Please refer to [Mono.Cecil web site](http://www.mono-project.com/docs/tools+libraries/libraries/Mono.Cecil/) for more information about Mono.Cecil which _CrossCutterN_ depends on for IL weaving.

## Contact Author

Should there be any issues or inquiries, please submit an issue to this project. Or alternatively, send an email to keeper013@gmail.com.  
Thanks
