# CrossCutterN: A light weight AOP tool for .NET

CrossCutterN is designed to allow .NET developers to inject customized AOP code into existing assemblies without the need of re-compilation of them. It allows .NET developers to inject customized AOP code at the point of **entry**, **exception** and **exit** of **methods** and **property getters and setters** via **custom attributes** and **name matching**.

## Quick Examples

:exclamation: In the examples, this article assumes that readers have cloned this project into there own environment and are able to compile and run the project binaries.

### AOP via Custom Attribute

For projects under development, AOP via custom attribute is a good way to inject customized AOP codes.

The following C# code (The code piece can be found in CrossCutterN.SampleTarget project):
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
To inject the following AOP code into Add1 method (The code piece can be found in CrossCutterN.SampleAdvice project)
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
1. Compile the whole project (Here we assume Debug configuration is used)
2. Copy _CrossCutterN.SampleAdvice/bin/Debug/CrossCutterN.SampleAdvice.dll_ to _CrossCutterN.Command/bin/Debug/_
3. Copy _CrossCutterN.SampleTarget/CrossCutterN.Command.exe.config_ to _CrossCutterN.Command/bin/Debug_, overwrite existing file with the same name, make sure the configuration contains following content:
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
4. In Windows Command Prompt, navigate to _CrossCutterN.Command/bin/Debug/_ directory, and execute
```batchfile
CrossCutterN.Command.exe ../../../CrossCutterN.SampleTarget/bin/Debug/CrossCutterN.SampleTarget.exe ../../../CrossCutterN.SampleTarget/bin/Debug/CrossCutterN.SampleTarget_Weaved.exe
```
5. The execution should be successful, a log file of weaving statistics should be generated under _CrossCutterN.Command/bin/Debug/ directory_
6. Nevigate to _CrossCutterN.SampleTarget/bin/Debug/_ directory, _CrossCutterN.SampleTarget_Weaved.exe_ should be generated there
7. Execute _CrossCutterN.SampleTarget_Weaved.exe_, something like the following result suggests that OnEntry and OnExit methods have been injected into _CrossCutterN.SampleTarget_ assembly
```
Entry at 2017-05-16 07:51:24.332 PM: Add1(x=1,y=2)
Inside Add1, starting
Inside Add1, ending
Exit at 2017-05-16 07:51:24.332 PM: returns 3
```
In the above example, the configuration section tells _CrossCutter.Command.exe_ tool to inject OnEntry and OnExit methods into all methods that has custom attribute _CrossCutterN.SampleAdvice.SampleConcernMethodAttribute_

### AOP via Name Matching

For assemblies that can't be re-compiled, this is a good way to inject AOP codes without the need of re-compilation

The steps are about the same, only a little difference of the code and configuration from above.  

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
Configuration file should contain the following section:
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

And the result is something like the following when executing _CrossCutterN.SampleTarget_Weaved.exe_
```
Entry at 2017-05-16 09:13:15.218 PM: Add2(x=1,y=2)
Inside Add1, starting
Inside Add1, ending
Exit at 2017-05-16 09:13:15.218 PM: returns 3
```
The configuration tells _CrossCutterN.Command.exe_ tool to inject OnEntry and OnExit methods into all methods that match name _"CrossCutterN.SampleTarget.Target.Add2"_

## Feature Summary

CrossCutterN.Command.exe tool is highly flexible and configurable. For detailed usage, please refer to CrossCutterN.Test project configuration file, or perhaps wiki pages of this project.


### AOP code injection via custom attribute

4 pre-defined attribute types are provided:
1. **Concern class attribute**: this attribute is valid for class as a mark that all methods and properties are subjects for AOP injection under that class, it has an _options_ element for detailed configuration of whether to inject a method or property due to it's accessibility and is static or not.
2. **Concern method attribute**: this attribute is valid for methods and propery getter/setters. The mentioned items will be injected if having this custom attribute. This attribute overwrites the custom attribute settings of the declaring class/property.
3. **Concern property attribute**: this attribute is valid for properties only. The property getters/setters will be injected if the properties have this attribute. This attribute overwrites the custom attribute settings of the declaring class.
4. **No Concern attribute**: this attribute is valid for methods/properties/property getters/property setters. The mentioned items and their child items (methods to class, getter/setter to property) will not be injected if having this attribute, unless overwritten by the custom attributes from their child items. This attributes overwrites the custom attribute settings of the declaring class/property.

 :exclamation: For the sake of keeping pre-defined attribute properties for injection logic, these 4 attributes must inherit from the corresponding attributes declared in _CrossCutterN.Concern_ project.
 
 Multiple injections via custom attribute can apply in the same configuration file, just add the elements under _concernAttributeAspectBuilders_ element.
 
 ### AOP code injection via name matching
 
AOP code injection via name matching supports include pattern and exclude pattern. If a method's/properties' name matches an excluded pattern, it won't be injected even if it also matches one of the include patterns.
 
Include/exclude patterns allow the use of asterisk character ("\*" character) as a wildcard to match any number of any characters, and this is the only supported wildcard for pattern matching for now.
 
The same as injection via attribute, multiple injection via name matching also can apply in the same configuration file, just add them under _nameExpressionAspectBuilders_ element.

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

## Contact

Should there be any issues or inquiries, please submit an issue to this project, or else, send an email to keeper013@gmail.com.  
Thanks
