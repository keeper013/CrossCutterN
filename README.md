# CrossCutterN: A Light Weight AOP Tool for .NET

[![Build Status](https://travis-ci.org/keeper013/CrossCutterN.svg?branch=master)](https://travis-ci.org/keeper013/CrossCutterN)

**_CrossCutterN_** is a free and lightweight AOP tool for .NET using IL weaving technology. It allows developers to inject custom AOP code into methods/properties of classes via custom attributes and method/property names, and switch on/off injected aspects at runtime. It is totally free, and is able to relieve projects from compile time dependencies on AOP code.

## Quick Examples

:exclamation: In the examples, this article assumes that readers have cloned this project into there own environment and are able to compile and run the project binaries.

### AOP Code Injection

The following C# code (The code piece can be found in _CrossCutterN.SampleTarget_ project):

```C#
namespace CrossCutterN.SampleTarget
{
    using System;
    using SampleAdvice;

    class Target
    {
        public static int Add(int x, int y)
        {
            Console.Out.WriteLine("Add starting");
            var z = x + y;
            Console.Out.WriteLine("Add ending");
            return z;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //CrossCutterN.Advice.Switch.SwitchFacade.Controller.SwitchOn(
            //  "AspectInjectedByAttributeExample");
            Target.Add(1, 2);
        }
    }
}
```

Outputs:

```batchfile
Add starting
Add ending
```

To output method name and parameter values upon method entry and return value upon method exit for Add function, _CrossCutterN_ tool have 2 ways to achieve the goal.

First of all, some AOP code is to be implemented to output the necessary information. Here for demonstration purpose, a very simple AOP code implementation is provided in project _CrossCuttern.SampleAdvice_:

```C#
namespace CrossCutterN.SampleAdvice
{
    using System;
    using System.Text;
    using Advice.Parameter;
    using Advice.Concern;

    public sealed class SampleConcernMethodAttribute : MethodConcernAttribute
    {
    }

    public static class Advices
    {
        public static void InjectByAttributeOnEntry(IExecution execution)
        {
            Console.Out.WriteLine("{0} Injected by attribute on entry: {1}", 
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"), GetMethodInfo(execution));
        }

        public static void InjectByAttributeOnExit(IReturn rReturn)
        {
            Console.Out.WriteLine("{0} Injected by attribute on exit: {1}", 
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"), GetReturnInfo(rReturn));
        }

        public static void InjectByMethodNameOnEntry(IExecution execution)
        {
            Console.Out.WriteLine("{0} Injected by method name on entry: {1}", 
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"), GetMethodInfo(execution));
        }

        public static void InjectByMethodNameOnExit(IReturn rReturn)
        {
            Console.Out.WriteLine("{0} Injected by method name on exit: {1}", 
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"), GetReturnInfo(rReturn));
        }

        private static string GetMethodInfo(IExecution execution)
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
            return strb.ToString();
        }

        private static string GetReturnInfo(IReturn rReturn)
        {
            return rReturn.HasReturn ? string.Format("returns {0}", rReturn.Value) : "no return";
        }
    }
}
```

Note that 4 public static methods are implemented, _InjectByAttributeOnEntry_ and _InjectByAttributeOnExit_ methods will be injected via _SampleConcernMethodAttribute_ defined together; _InjectByMethodNameOnEntry_ and _InjectByMethodNameOnExit_ methods will be injected by the method name "Add".

**_CrossCutterN_** tool assumes that AOP code assemblies should reference _CrossCutterN.Advice.dll_ assembly to correctly support the injection behavior. IExecution, IReturn and other necessary and supportive interfaces and implementations are defined in CrossCutterN.Advice.dll assembly. This assembly should be copied over together with AOP code assembly to work together with injected assemblies.

**_CrossCutterN_** tool utilizes a console application project _CrossCutterN.Command_ to do the AOP code injection. Before executing it, it's configuration file CrossCutterN.Command.exe.config must be properly set up for the AOP code injection process (In sample program it's already prepared in CrossCutterN.Command directory):

The content of _CrossCutterN.Command.exe.config_ is like the following:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="crossCutterN" type="CrossCutterN.Command.Configuration.CrossCutterNSection, CrossCutterN.Command" allowLocation="true" allowDefinition="Everywhere" />
  </configSections>
  <crossCutterN>
    <concernAttributeAspectBuilders>
      <add id="AspectInjectedByAttributeExample">
        <factoryMethod type="CrossCutterN.Aspect.Builder.AspectBuilderFactory, CrossCutterN.Aspect" method="InitializeConcernAttributeAspectBuilder" methodConcernAttribute="CrossCutterN.SampleAdvice.SampleConcernMethodAttribute, CrossCutterN.SampleAdvice"/>
        <pointcut>
          <add joinPoint="Entry" sequence="1" classType="CrossCutterN.SampleAdvice.Advices, CrossCutterN.SampleAdvice" method="InjectByAttributeOnEntry" parameterPattern="Execution"/>
          <add joinPoint="Exit" sequence="2" classType="CrossCutterN.SampleAdvice.Advices, CrossCutterN.SampleAdvice" method="InjectByAttributeOnExit" parameterPattern="Return"/>
        </pointcut>
        <!--<switch status="Off"/>-->
      </add>
    </concernAttributeAspectBuilders>
    <nameExpressionAspectBuilders>
      <add id="AspectInjectedByMethodNameExample">
        <factoryMethod type="CrossCutterN.Aspect.Builder.AspectBuilderFactory, CrossCutterN.Aspect" method="InitializeNameExpressionAspectBuilder">
          <includes>
            <add expression="CrossCutterN.SampleTarget.Target.Ad*"/>
          </includes>
        </factoryMethod>
        <pointcut>
          <add joinPoint="Entry" sequence="2" classType="CrossCutterN.SampleAdvice.Advices, CrossCutterN.SampleAdvice" method="InjectByMethodNameOnEntry" parameterPattern="Execution"/>
          <add joinPoint="Exit" sequence="1" classType="CrossCutterN.SampleAdvice.Advices, CrossCutterN.SampleAdvice" method="InjectByMethodNameOnExit" parameterPattern="Return"/>
        </pointcut>
      </add>
    </nameExpressionAspectBuilders>
  </crossCutterN>
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5.2" />
  </startup>
</configuration>
```
In the above configuration:

One aspect builder with id "**AspectInjectedByAttributeExample**" is added to **concernAttributeAspectBuilders** collection. According to the value of it's configuration property **methodConcernAttribute**, any method (property getter and setter as well) marked by the attribute class it's value represents will be injected upon entry and exit with the AOP methods defined in its Pointcut collection. To apply this aspect builder to Add method, apply **[SampleConcernMethod]** attribute to it defined in _CrossCutterN.SampleTarget.Target_ class.

Similarly, one aspect builder with id "**AspectInjectedByMethodNameExample**" is added to **nameExpressionAspectBuilders** collection, according to the setting of which, any method whose full name matches "**CrossCutterN.SampleTarget.Target.Ad\***" will be injected upon entry and exit with the AOP methods defined in its **Pointcut** collection.

Compile the whole project using release build, then in windows command prompt, nevigate to path _CrossCutterN/CrossCutterN.Command/bin/Release_ and execute the following commands:

```batchfile
copy ..\..\..\CrossCutterN.SampleAdvice\bin\Release\CrossCutterN.SampleAdvice.dll .
copy ..\..\..\CrossCutterN.SampleTarget\bin\Release\CrossCutterN.SampleTarget.exe .
copy ..\..\..\CrossCutterN.SampleTarget\bin\Release\CrossCutterN.SampleTarget.pdb .
CrossCutterN.Command.exe CrossCutterN.SampleTarget.exe CrossCutterN.SampleTarget_Weaved.exe Y
```

Note that since we are injecting code implemented in project _CrossCutterN.SampleAdvice_, the _CrossCutterN.SampleAdvice.dll_ assembly must be available in _CrossCutterN.Command.exe_'s search path, most conveniently just copy it to the same directory. The last command tells _CrossCutterN.Command.exe_ command to load _CrossCutterN.SampleTarget.exe_ assembly, inject it with the AOP code according to the content in _CrossCutterN.Command.exe.config_ file (which will be mentioned later), and output the result as assembly _CrossCutterN.SampleTarget_Weaved.exe_. The "Y" parameter means handling pdb file as well, this parameter is optional, and is only applicable when the target pdb file is within the same folder of the assembly being injected, and very useful if the injected assembly needs to be debugged. Note that an injection log file named like "Wave_yyyy_MM_dd_HH_mm_ss_fff.log" should be generated in the same folder which contains the injection summary.

After the last command is successfully executed, try to execute _CrossCutterN.SampleTarget_Weaved.exe_, the extra output than original suggests that AOP code has been injected successfully.

```batchfile
2017-05-31 10:30:47.581 PM Injected by attribute on entry: Add(x=1,y=2)
2017-05-31 10:30:47.591 PM Injected by method name on entry: Add(x=1,y=2)
Add starting
Add ending
2017-05-31 10:30:47.591 PM Injected by method name on exit: Add(x=1,y=2)
2017-05-31 10:30:47.595 PM Injected by attribute on exit: Add(x=1,y=2)
```

Injection by attribute is designed for development on going projects to allow developers to separate AOP code from business code, and easily apply aspects by attributes.  
Injection by method name is more designed for built assemblies of which recompile actions are limited or impossible.

### Aspect Switching

To provide operational convenience (e.g. trouble shooting), In case that occationaly some injected aspects need to be turned off for a while and later turned on after problems are solved or when necessary, **_CrossCutterN_** provides runtime aspect switching feature.

Note the commented out line in config file of _CrossCutterN.Command.exe_:

```xml
<!--<switch status="Off"/>-->
```

Uncomment it, execute the last command (this time it's not necessary to copy over the dll and exe and pdb files if they are there already) to generate _CrossCutterN.SampleTarget_Weaved.exe_ file using the updated configuration. After that execute _CrossCutterN.SampleTarget_Weaved.exe_, the result suggests that injected feature by aspect builder with id "**AspectInjectedByAttributeExample**" is not applied anymore.

```batchfile
2017-05-31 10:32:39.018 PM Injected by method name on entry: Add(x=1,y=2)
Add starting
Add ending
2017-05-31 10:32:39.034 PM Injected by method name on exit: Add(x=1,y=2)
```
To switch it on at run time, note the commented out lines in Main method of project _CrossCutterN.SampleTarget_:

```C#
//CrossCutterN.Advice.Switch.SwitchFacade.Controller.SwitchOn(
//  "AspectInjectedByAttributeExample");
```

Uncomment it, rebuild the project, execute the commands again (No need to copy over _CrossCutterN.SampleAdvice.dll_ if it's there already, it is not updated), and execute _CrossCutterN.SampleTarget_Weaved.exe_, the result suggests that the injected feature by aspect builder with id "**AspectInjectedByAttributeExample**" is switched on.

```batchfile
2017-05-31 10:36:27.727 PM Injected by attribute on entry: Add(x=1,y=2)
2017-05-31 10:36:27.742 PM Injected by method name on entry: Add(x=1,y=2)
Add starting
Add ending
2017-05-31 10:36:27.742 PM Injected by method name on exit: Add(x=1,y=2)
2017-05-31 10:36:27.742 PM Injected by attribute on exit: Add(x=1,y=2)
```

## Feature Summary

_CrossCutterN.Command.exe_ tool is highly flexible and configurable. For more complete usage examples, please refer to content in _CrossCutterN.Test_ project.

### Write pdb files

To make injected assemblies debuggable, pdb files needs to be overwritten while injecting to the assemblies. To overwrite pdb files, add a "Y" parameter after input and output file paths. so the command looks like the following:

```batchfile
CrossCutterN.Command.exe <input assembly path> <output assembly path> Y
```
To make sure the above command can be successful, the pdb file should be at the same directory with the assembly to be injected, and their names except extension should be the same (e.g. _CrossCutterN.SampleTarget.exe_ and _CrossCutterN.SampleTarget.pdb_).

Any other value than "Y" or no value for the 3rd command line parameter will cause the pdb file not overwritten. Of course, for some cases that there is no pdb file to overwrite, leaving to be empty or putting other values than "Y" is the only choice.

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
| (CrossCutterN.Advice.Parameter.IExecutionContext) | Context |
| (CrossCutterN.Advice.Parameter.IExecution) | Execution |
| (CrossCutterN.Advice.Parameter.IExecutionContext, CrossCutterN.Advice.Parameter.IExecution) | ContextExecution |

#### Exception

| Parameter List Pattern | Configuration Value |
| --- | --- |
| () | Empty |
| (CrossCutterN.Advice.Parameter.IExecutionContext) | Context |
| (CrossCutterN.Advice.Parameter.IExecution) | Execution |
| (System.Exception) | Exception |
| (CrossCutterN.Advice.Parameter.IExecutionContext, CrossCutterN.Advice.Parameter.IExecution) | ContextExecution |
| (CrossCutterN.Advice.Parameter.IExecutionContext, System.Exception) | ContextException |
| (CrossCutterN.Advice.Parameter.IExecution, System.Exception) | ExecutionException |
| (CrossCutterN.Advice.Parameter.IExecutionContext, CrossCutterN.Advice.Parameter.IExecution, System.Exception) | ContextExecutionException |

#### Exit

| Parameter List Pattern | Configuration Value |
| --- | --- |
| () | Empty |
| (CrossCutterN.Advice.Parameter.IExecutionContext) | Context |
| (CrossCutterN.Advice.Parameter.IExecution) | Execution |
| (CrossCutterN.Advice.Parameter.IReturn) | Return |
| (bool) | HasException |
| (CrossCutterN.Advice.Parameter.IExecutionContext, CrossCutterN.Advice.Parameter.IExecution) | ContextExecution |
| (CrossCutterN.Advice.Parameter.IExecutionContext, CrossCutterN.Advice.Parameter.IReturn) | ContextReturn |
| (CrossCutterN.Advice.Parameter.IExecutionContext, bool) | ContextHasException |
| (CrossCutterN.Advice.Parameter.IExecution, CrossCutterN.Advice.Parameter.IReturn) | ExecutionReturn |
| (CrossCutterN.Advice.Parameter.IExecution, bool) | ExecutionHasException |
| (CrossCutterN.Advice.Parameter.IReturn, bool) | ReturnHasException |
| (CrossCutterN.Advice.Parameter.IExecutionContext, CrossCutterN.Advice.Parameter.IExecution, CrossCutterN.Advice.Parameter.IReturn) | ContextExecutionReturn |
| (CrossCutterN.Advice.Parameter.IExecutionContext, CrossCutterN.Advice.Parameter.IExecution, bool) | ContextExecutionHasException |
| (CrossCutterN.Advice.Parameter.IExecutionContext, CrossCutterN.Advice.Parameter.IReturn, bool) | ContextReturnHasException |
| (CrossCutterN.Advice.Parameter.IExecution, CrossCutterN.Advice.Parameter.IReturn, bool) | ExecutionReturnHasException |
| (CrossCutterN.Advice.Parameter.IExecutionContext, CrossCutterN.Advice.Parameter.IExecution, CrossCutterN.Advice.Parameter.IReturn, bool) | ContextExecutionReturnHasException |

* _CrossCutterN.Advice.Parameter.IExecutionContext_ parameter is for passing objects among advice methods, it has Get, Set, Remove and Exist method for retrieving, storing, rmoving and looking up for objects that are supposed to be passed among entry, exception or exit advices with object keys.
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
	    // these switches are for switching aspects on and off
            // assume aspects 2, 4, 6, 7, 11, 13, 15 and 16 are switchable, which makes IReturn parameter switchable
            bool switch2 = <pre-initialized value>;
            bool switch4 = <pre-initialized value>;
            bool switch6 = <pre-initialized value>;
            bool switch7 = <pre-initialized value>;
	    bool switch11 = <pre-initialized value>;
	    bool switch13 = <pre-initialized value>;
	    bool switch15 = <pre-initialized value>;
	    bool switch16 = <pre-initialized value>;
            // The following local variables will only be initialized if needed for injected advices
	    // execution context is for passing object among advices, not switchable because some advices that needs it are not switchable
	    var executionContext = CrossCutterN.Advice.Parameter.ParameterFactory.InitializeExecutionContext();
	    // executionParameter local variable is not switchable because EntryAdvice1 advice needs it which is in an not switchable aspect
            var executionParameter = CrossCutterN.Advice.Parameter.ParameterFactory.InitializeExecution(
                "CrossCutterN.SampleTarget, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", 
                "CrossCutterN.SampleTarget", 
                "CrossCutterN.SampleTarget.Sample", 
                "Sample", 
                "System.String CrossCutterN.SampleTarget.Sample::SampleMethod(System.String,System.Text.StringBuilder)", 
                "SampleMethod", 
                "System.String");
            executionParameter.AddParameter(
                CrossCutterN.Advice.Parameter.ParameterFactory.InitializeParameter("x", "System.Int32", 0, x).Convert());
            executionParameter.AddParameter(
                CrossCutterN.Advice.Parameter.ParameterFactory.InitializeParameter("strb", "System.Text.StringBuilder", 1, strb).Convert());
            var execution = executionParameter.Convert();
	    // if all switches are switched off, returnParameter may not be necessary to be initialized
            // this is an optimization done during il weaving process
            // the same optimization is implemented for execution parameter as well, if all aspects that need it are switchable
            IWriteOnlyReturn returnParameter;
            if (switch2 || switch4 || switch6 || switch7 || switch11 || switch13 || switch15 || switch16)
            {
                returnParameter = CrossCutterN.Advice.Parameter.ParameterFactory.InitializeReturn(true, "System.String");
            }
            string returnValue = null;
	    // there are advices that need hasException parameter that aren't switchable, so this local variable is not switchable, or else is will be
	    // considering hasException is only one bool, switching it consumes more computing resouse, we don't switch this parameter
            var hasException = false;
            try
            {
                // Injected Entry Advices
                EntryAdvice1(execution);
                if (switch2)
                {
                    EntryAdvice2();
                }
		EntryAdvice3(executionContext, execution);
		EntryAdvice4(executionContext);
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
	        // again, we don't switch hasException because it's not worthy of the performance cost
                hasException = true
                // Injected Exception Advices
                ExceptionAdvice1(execution);
                if (switch2)
                {
                    ExceptionAdvice2(execution, e);
                }
                ExceptionAdvice3(e);
                if (switch4)
                {
                    ExceptionAdvice4();
                }
		ExceptionAdvice5(executionContext, execution);
		ExceptionAdvice6(executionContext, exception);
		ExceptionAdvice7(executionContext);
		ExceptionAdvice8(executionContext, execution, exception);
                ...
		// CrossCutterN will rethrow the exception to keep the original behaivor
		throw;
            }
            finally
            {
	    	// again, this statement is switchable if all advices that need IReturn and all advices that need hasException are switchable
                IReturn rtn;
		// here is an example of switching off operation of a added local variable if all relevant advices are switched off
                if (switch2 || switch4 || switch6 || switch7 || switch11 || switch13 || switch15 || switch16)
                {
		    returnParameter.HasReturn = !hasException;
		    if (!hasException)
                    {
                        returnParameter.Value = returnValue;
                    }
                    rtn = returnParameter.Convert();
                }
                // Injected Exit Advices
                ExitAdvice1(execution);
                if (switch2)
                {
                    ExitAdvice2(rtn);
                }
                ExitAdvice3(executionContext.ExceptionThrown);
                if (switch4)
                {
                    ExitAdvice4(execution, rtn);
                }
                ExitAdvice5(execution, executionContext.ExceptionThrown);
                if (switch6)
                {
                    ExitAdvice6(rtn, executionContext.ExceptionThrown);
                }
                if (switch7)
                {
                    ExitAdvice7(execution, rtn, executionContext.ExceptionThrown);
                }
		ExitAdvice8();
		ExitAdvice9(executionContext);
		ExitAdvice10(executionContext, execution);
		if (switch11) 
		{
		    ExitAdvice11(executionContext, rtn);
		}
		ExitAdvice12(executionContext, hasException);
		if (switch13) 
		{
		    ExitAdvice13(executionContext, execution, rtn);
		}
		ExitAdvice14(executionContext, execution, hasException);
		if (switch15) 
		{
		    ExitAdvice15(executionContext, rtn, hasException);
		}
		if (switch16) 
		{
		    ExitAdvice16(executionContext, execution, rtn, hasException);
		}
                ...
            }
        }
    }
}
```
Only the injected portion is done by IL weaving instead of written in C# code. After a successful injection, all a developer needs to do is to copy _CrossCutterN.Advice_ assembly and the assembly that contains the declaration and implementation of injected methods (like EntryAdvice1, ExitAdvice7 in the represented code above) in to the search path of the running program (most conveniently the same folder with the injected assembly), to make sure necessary assemblies can be found by the program.

### Object Passing

Parameter IExecutionContext is designed for passing objects amoung injected advices, in case required. Any object saved in one advice may be accessed, updated or deleted by other adviced called later, even if the advices may not come from the same aspect builder. In other words, the IExecutionContext is shared among all advices. This design is for flexibility concern.

When using this feature, please be sure to choose the key for each object in each aspect carefully, if different aspects aren't supposed to access object saved by other aspects, don't overwrite other aspects' objects by mistake.

### Multiple Injections to the Same Method/Property 

Considering in real life development, multiple concerns may apply to the same method/property (logging, validation, authorization and so on), _CrossCutterN_ is designed to allow such use cases. Injection for one concern is described as one **_AspectBuilder_**, which can generate one **_Aspect_** for a method/property to be injected. One **_Aspect_** may contain one **_advice_** for each join point listed above. When injecting to one target method/property, all **_AspectBuilder_** s will sequentially generate **_Aspect_** s for the target, then each **_advice_** in the **_Aspect_** s will be summarized for each join point and injected to the join point of the target sequentially according to the sequence number applied to the **_AspectBuilder_** which the **_advice_** comes from.  
In configuration file, sequence number must be applied to each join point configuration element. For the same join point, sequence number of each **_AspectBuilder_** must be different, which is enforced by validation during injection process.

![Multiple Aspect Builders](https://github.com/keeper013/CrossCutterN/blob/master/MultipleAspectBuilder.png)

### Convenient Injection Control

To allow developers to easily include most methods/properties and exclude certain methods/properties in a class, AOP code injection via custom attribute is designed to be overwritable by hierachy.

Four pre-defined attribute types are provided:

**_CrossCutterN.Advice.Concern.ClassConcernAttribute_**: this attribute is valid for classes that all methods and properties are subjects for AOP injection within the classes which has this attribute. It contains following properties

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

**_CrossCutterN.Advice.Concern.MethodConcernAttribute_**: this attribute is valid for methods and propery getters/setters. The mentioned items will be injected if having this custom attribute, regargless if ConcernMethod/ConcernPropertyGetter/ConcernPropertySetter attribute properties are set to false in the class concern attribute, or if there is no class concern attribute applied to the class. It contains following properties:

| Property | Description | Default Value |
| --- | --- | --- |
| PointCutAtEntry | if set to false, injection to Entry join point will be disabled | true |
| PointCutAtException | if set to false, injection to Exception join point will be disabled | true |
| PointCutAtExit | if set to false, injection to Exit join point will be disabled | true |

These attribute properties will overwrite the attribute properties with the same name in concern class attribute or concern property value.

**_CrossCutterN.Advice.Concern.PropertyConcernAttribute_**: this attribute is valid for properties only. The property getters/setters will be injected if the properties have this attribute, regardless if \ConcernPropertyGetter/ConcernPropertySetter attribute properties are set to false in the class concern attribute, or if there is no class concern attribute applied to the class. It contains following properties:

| Property | Description | Default Value |
| --- | --- | --- |
| ConcernPropertyGetter | if set to false, getter of the property won't be injected | false |
| ConcernPropertySetter | if set to false, setters of the property won't be injected | false |
| PointCutAtEntry | if set to false, injection to Entry join point will be disabled | true |
| PointCutAtException | if set to false, injection to Exception join point will be disabled | true |
| PointCutAtExit | if set to false, injection to Exit join point will be disabled | true |

These attribute properties will overwrite the attribute properties with the same name in concern class attribute.

**_CrossCutterN.Advice.Concern.NoConcernAttribute_**: this attribute is valid for methods/properties/property getters/property setters. The mentioned items and their child items (methods to class, getter/setter to property) will not be injected if having this attribute, unless overwritten. It has no properties.

:exclamation: The pre-defined attributes are all declared as abstract to force developers to inherite from them and use their own custom attributes for AOP code injection. The reason for this is to avoid re-using of the same existing attribute for multiple concern injections. It is encouraged to have dedicated set of attributes for different concern's injection.

:exclamation: For the sake of keeping pre-defined attribute properties for injection logic, custom implementation of these 4 attributes must inherit from the above properties. This is enforced by validation of parent class type during injection process.

:exclamation: Developers are not requested to inherit from all 4 attributes above to build a complete set of attributes. For example, if only few methods needs to be injected, only inheriting **_CrossCutterN.Concern.Attribute.MethodConcernAttribute_** is good enough. However, at least on of **_CrossCutterN.Concern.Attribute.ClassConcernAttribute_**, **_CrossCutterN.Concern.Attribute.MethodConcernAttribute_** and **_CrossCutterN.Concern.Attribute.PropertyConcernAttribute_** should be inherited to pass the validation of **_ConcernAttributeAspectBuilder_**.

:exclamation: Note that in configuration section, **_ConcernAttributeAspectBuilder_** and **_NameExpressionAspectBuilder_** have similar configuration settings as the attribute properties listed above. They are **not** the same. The configuration options in the configuration file are default settings of **_AspectBuilder_** s, in case the value of the corresponding attribute property is not defined. Hard coded attribute properties, if exist, overwrites the default settings of the **_AspectBuilder_** s.

Available configuration options for **_ConcernAttributeAspectBuilder_** with default values are listed below:

| Configuration Option | Description | Default Value |
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
 
 ### AOP Code Injection via Name Matching
 
Including/excluding patterns allow the usage of asterisk character ("\*" character) as a wildcard to match any number of any characters, and this is the only supported wildcard character for pattern matching for now.

Available configuration options for **_NameExpressionAspectBuilder_** with default values are listed below:

| Configuration Option | Description | Default Value |
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

Note that it doesn't have _PointCutAtEntry_, _PointCutAtException_ and _PointCutAtExit_ options. If this **_AspectBuilder_** is not supposed to inject to a certain join point, simply don't add the advice for that join point will do.

AOP code injection via name matching supports including and excluding method/property name patterns, and other options as ConcernPublic, Concern Static as mentioned above. The matching rules are as following:

* if the method/property is matched by one of the including patterns which contains wildcards
 * if the method/property fits the option settings (public method/property matches ConcernPublic = true and so on), it will be injected.
 * if the method/property doesn't fit the option settings (public method/property matches ConcernPublic = true and so on), it will not be injected.
* if the method/property is matched by one of the excluding patterns, it will not be injected even if it is also matched by one of the including patterns that contains wildcards.
* if the method/property is matched by one of the including patterns which doesn't contain wildcards, it will be injected, regardless of any other configurations or settings, which is called "Exact match takes priority" rule.

### Aspect Switching

#### Configuration

Currently both **NameExpressionAspectBuilder** and **ConcernAttributeAspectBuilder** support aspect switching, and the configuration elment are the same as in the quick demonstration. There are 3 available values for **status** configuration propety of **switch** element:
* On: The aspect is switchable, by default turned on
* Off: The aspect is switchable, by default turned off
* NotSwitchable: The aspect is not switchable, it will always be executed, can't be turned off/on. This is the default value, if **switch** element is not specified, this value will be taken.

#### Interface

**_CrossCutterN_** allows to switch aspects with methods listed below, all listed method are implemented under interface _CrossCutterN.Advice.Switch.IAdviceSwitch_, which is exposed as static property _Controller_ of static class _CrossCutterN.Advice.Switch.SwitchFacade_

| Method | Description |
| --- | --- |
| Switch(Type type) | Switch all aspects applied to all methods and all properties under the class, on=>off, off=>on |
| Switch(MethodInfo method) | Switch all aspects applied to the method, on=>off, off=>on |
| Switch(PropertyInfo property) | Switch all aspects applied to getter and setter (if exists) of the property, on=>off, off=>on |
| Switch(string aspect) | Switch the aspect, on=>off, off=>on |
| Switch(Type type, string aspect) | Switch the aspect applied to the class, on=>off, off=>on |
| Switch(MethodInfo method, string aspect) | Switch the aspect applied to the method, on=>off, off=>on |
| Switch(PropertyInfo property, string aspect) | Switch the aspect applied to getter and setter (if exists) of the property, on=>off, off=>on |
| SwitchOn(Type type) | Switch on all aspects applied to all methods and all properties under the class |
| SwitchOn(MethodInfo method) | Switch on all aspects applied to the method |
| SwitchOn(PropertyInfo property) | Switch on all aspects applied to getter and setter (if exists) of the property |
| SwitchOn(string aspect) | Switch on the aspect |
| SwitchOn(Type type, string aspect) | Switch on the aspect applied to the class |
| SwitchOn(MethodInfo method, string aspect) | Switch on the aspect applied to the method |
| SwitchOn(PropertyInfo property, string aspect) | Switch on the aspect applied to getter and setter (if exists) of the property |
| SwitchOff(Type type) | Switch off all aspects applied to all methods and all properties under the class |
| SwitchOff(MethodInfo method) | Switch off all aspects applied to the method |
| SwitchOff(PropertyInfo property) | Switch off all aspects applied to getter and setter (if exists) of the property |
| SwitchOff(string aspect) | Switch off the aspect |
| SwitchOff(Type type, string aspect) | Switch off the aspect applied to the class |
| SwitchOff(MethodInfo method, string aspect) | Switch off the aspect applied to the method |
| SwitchOff(PropertyInfo property, string aspect) | Switch off the aspect applied to getter and setter (if exists) of the property |

Each of the switch operation listed above will overwrite any previous operation results. For example, if there is an aspect build with id "**TestAspectBuilder**", which applied advice to "_Minus_" method of "_TestMath_" class, and the aspect builder by default turns on aspects. Then:

1. Call Switch("TestAspectBuilder"), then the all advices applied by **TestAspectBuilder** will be switched, in this case switched off (because by default the status is turned on, and there are no previous switching operations) including the aspect mentioned in the example
2. Call SwitchOn(_TestMath_), then all aspects applied to "_TestMath_" class will be turned on, including the aspect mentioned in the example
3. Assume "_TestProperty_" is a property of class "_TestMath_", calling SwitchOff(_TestProperty_) won't change the aspect switch status mentioned in the example, because it's not relevant
4. Call Switch(_Minus_), the aspect mentioned in the example will be turned off, because previously it's switch status was switched on
5. Call SwitchOff(_TestMath_, "TestAspectBuilder"), the aspect mentioned in the example will be turned off again, so the status is still turned off.

Please note that the smallest granularity of aspect switching is aspect applied to a method/property getter/property setter. **_CrossCutterN_** doesn't support switching individual advices in an aspect (e.g. If an aspect builder injects advices on entry and exit to a method, the individual advice on entry can't be switched without switching the on exit advice together).

IAspectSwitch interface also provides one method for user to check the switch status of one aspect applied on one method/property getter/property setter:

```c#
bool? GetSwitchStatus(MethodInfo method, string aspect);
```

True or false return suggests the switch status, if return has no value, it suggests that the switch doesn't exist, maybe the aspect applied isn't switchable, or the aspect is not applied to the method, or the aspect is supposed to be applied to the method, but the class which defines the method isn't loaded yet.

#### Other Concerns

There is a concern that when a program is executing, there is no way to tell when a specific class is loaded. So what happens if aspects applied to the not loaded classes are switched before they actually get loaded?

The answer is: **_CrossCutterN_** tool will record the switching statuses in case some injected classes haven't been loaded into program, and apply the switching history once the class is loaded. This switching status recording process is optimized so that not each and every switching operation is kept in memory before loading the class, but only the minimum switching statuses are recorded to make sure if loading classes after some switching operation, it behaves the same as if the classes have been loaded before any aspect switching happens.

For multi-threading concern, aspect switching feature is designed for multi-threading use cases, switching operation and switch look up operation (carried out by injected IL) can happen in multiple threads without causing problems of the injected program.

## Project Structure
* **_CrossCutterN.Advice_**: Basic support assembly for _CrossCutterN_ tool. Please make sure that this assembly is copied over to the directories where injected assemblies are deployed. It contains pre-defined advice parameters, common supports, base attributes for _ConcernAttributeAspectBuilder_ and so on.
* **_CrossCutterN.Aspect_**: Contains the definition of the 2 ways of AOP code injection. _CrossCutterN.Command_ tool depends on this assembly. Also, to have customized ways for AOP code injection, this assembly is also the extension point.
* **_CrossCutterN.Weaver_**: the module that does the code injection using _Mono.Cecil_ IL weaving technologies. _CrossCutterN.Command_ tool depends on this assembly, and this assembly is not a designed extension point.
* **_CrossCutterN.Command_**: the executable assembly of CrossCutterN tool, which loads assemblies and output injected assemblies. The configuration definition is included in it.
* **_CrossCutterN.Test_**: Unit test project for CrossCutterN. AfterBuild target within the project is filled in with msbuild/xbuild scripts to copy _CrossCutterN.Command.exe_ and it's dependencies into output directory of this project, execute it to generate a injected _CrossCutterN.Test.dll_, replace the original _CrossCutterN.Test.dll_ with it, and clean up _CrossCutterN.Command.exe_ and the dependencies from the output directory. So unit test can be carried out directly after build is done, and execution of _CrossCutterN.Command.exe_ is transparent in the build process.
* **_CrossCutterN.SampleAdvice_**: Sample AOP code for a quick demonstration of the tool.
* **_CrossCutterN.SampleTarget_**: Sample assembly to be injected for a quick demonstration of the tool.

## Usage Attention

:exclamation: Please don't use this tool to inject the already injected assemblies. Take the assembly _CrossCutterN.SampleTarget_Weaved.exe_ mentioned in the quick demonstration for example, if this assembly is injected again using CrossCutterN tool, there is no guarantee that it still works perfectly.  

:exclamation: There is no guarantee that CrossCutterN works with any other AOP tools.  

:exclamation: There is no point to do this AOP code injection process using multi-thread style, for developers tend to develop their own tools based on CrossCutterN source code, please be reminded that the AOP code injection part isn't designed for multi-threading at all (why would someone want 2 thread to inject one assembly).  

:exclamation: There is no guarantee that CrossCutterN works with obfuscation tools.

## Extension

If 2 implemented **_AspectBuilder_** s are not sufficient, developers can implement their own aspect builder by implementing _CrossCutterN.Aspect.Builder.IAspectBuilder_ interface defined in assembly _CrossCutterN.Aspect_. Please look into the source code of the assembly to find out about all relevant interfaces and implementations

However, to integrate the newly implemented **_AspectBuilder_** into _CrossCutterN.Command_ tool, source code of _CrossCutterN.Command_ needs to be updated, if the some customized configuration in the configuration file is required.

## References

Please refer to [AOP Wikipedia](https://en.wikipedia.org/wiki/Aspect-oriented_programming) for background knowledge of AOP.

Please refer to [Mono.Cecil web site](http://www.mono-project.com/docs/tools+libraries/libraries/Mono.Cecil/) for more information about Mono.Cecil which _CrossCutterN_ depends on for IL weaving.

## Contact Author

Should there be any issues, suggestions or inquiries, please submit an issue to this project. Or alternatively, send an email to keeper013@gmail.com.  
Thanks
