namespace DataPS
{
    internal class ReflectionUtility
    {

        // based partially on : http://stackoverflow.com/questions/1253725/convert-ienumerable-to-datatable

        internal static System.Func<T, object> GetGetter<T>(System.Reflection.PropertyInfo property)
        {
            // get the get method for the property
            var method = property.GetGetMethod(true);

            // get the generic get-method generator (ReflectionUtility.GetSetterHelper<TTarget, TValue>)
            var genericHelper = typeof (ReflectionUtility).GetMethod(
                "GetGetterHelper",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            // reflection call to the generic get-method generator to generate the type arguments
            var constructedHelper = genericHelper.MakeGenericMethod(
                method.DeclaringType,
                method.ReturnType);

            // now call it. The null argument is because it's a static method.
            object ret = constructedHelper.Invoke(null, new object[] {method});

            // cast the result to the action delegate and return it
            return (System.Func<T, object>) ret;
        }

        private static System.Func<object, object> GetGetterHelper<TTarget, TResult>(System.Reflection.MethodInfo method)
            where TTarget : class // target must be a class as property sets on structs need a ref param
        {
            // Convert the slow MethodInfo into a fast, strongly typed, open delegate
            var func =
                (System.Func<TTarget, TResult>)
                System.Delegate.CreateDelegate(typeof (System.Func<TTarget, TResult>), method);

            // Now create a more weakly typed delegate which will call the strongly typed one
            System.Func<object, object> ret = (object target) => (TResult) func((TTarget) target);
            return ret;
        }
    }
}
