/**
 * Description: can convert to read only interface, made for write only interfaces
 * Author: David Cui
 */
 
 namespace CrossCutterN.Advice.Common
{
    public interface ICanConvertToReadOnly<out T>
    {
        T ToReadOnly();
    }
}
