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

The configuration tells _CrossCutterN.Command.exe_ tool to inject OnEntry and OnExit methods into all methods that match name _"CrossCutterN.SampleTarget.Target.Add2"_

## Feature Summary

CrossCutterN.Command
