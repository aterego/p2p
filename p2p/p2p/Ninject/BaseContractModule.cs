using System;
using System.Collections.Generic;
using System.Text;
using p2p.Views;

namespace p2p
{
    #region Usings
    using Ninject.Modules;
    using p2p.Models;
    using p2p.Services;
    #endregion
    public class BaseContractModule : NinjectModule
    {
        public override void Load()
        {
            Bind<LoginPage>().ToSelf().InSingletonScope();
            Bind<CustomerView>().ToSelf().InSingletonScope();

            Bind<Test>().ToSelf().InSingletonScope();
            Bind<IBackendProxy>().To<BackendProxy>().InSingletonScope();
            Bind<IBackendSessionManager>().To<BackendSessionManager>().InSingletonScope();
        }
    }
}
