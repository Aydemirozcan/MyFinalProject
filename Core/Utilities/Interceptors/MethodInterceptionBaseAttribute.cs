using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]             //Classlara ve metotlara kullanabilirsin.Birden fazla kullanabilirsin.Ve inherit edilen bir yerde bu attribute çalışsın.
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor                 //IInterceptor bize DynamicProxy den geliyor.Autofac in Interceptor özelliği vardır.
    {
        public int Priority { get; set; }                                //Priority öncelik demektir.Hangisi öncelikli çalışsın onu belirtmek için.(Hangi Attribute önce çalışsın sıralama yapmak istersek.)

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }



}
