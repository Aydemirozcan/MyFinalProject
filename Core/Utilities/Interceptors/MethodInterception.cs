using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute                 //Burası artık bizim bütün metotlarımızın çatısı.Çalıştırdığımız metotlar önce bu aşağıdaki kurallardan geçecek.
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation)               //Buradaki invocation bizim metodumuz oluyor(örneğin add)
        {
            var isSuccess = true;
            OnBefore(invocation);                                           //Metotdan önce çalışsın.                    
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);                                 //Metod hata aldığında çalışsın.
                throw;
            }
            finally              
            {
                if (isSuccess)                                          //Metod başarılı olursa bu çalışsın.
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);                                      //Metoddan sonra çalışsın istersek bu çalışsın.
        }                                                       
    }                                                                 //Biz genellikle OnBefore ve OnExceptionı kullanırız. 



}
