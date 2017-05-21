/**
 * Description: can convert to read only interface, made for write only interfaces
 * Author: David Cui
 */
 
 namespace CrossCutterN.Advice.Common
{
    public interface ICanConvert<out T>
    {
        T Convert();
    }
}
