[assembly: WebActivator.PreApplicationStartMethod(typeof(DVSE.Web.HolidayManagement.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(DVSE.Web.HolidayManagement.App_Start.NinjectWebCommon), "Stop")]

namespace DVSE.Web.HolidayManagement.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using DVSE.DAL.HolidayManagement.EF.UnitOfWork;
    using DVSE.DAL.HolidayManagement.EF;
    using GenericRepository.EntityFramework;
    using DVSE.Web.HolidayManagement.Infrastructure.Authentication;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static IKernel Kernel { get; set; }

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));

            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);

            Kernel = kernel;

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDomainUserProvider>().To<FakeDomainUserProvider>().InSingletonScope();
            kernel.Bind<IEntitiesContext>().To<HMContext>().InRequestScope();
            kernel.Bind<IHMUnitOfWork>().To<HMUnitOfWork>().InRequestScope ();
        }        
    }
}
