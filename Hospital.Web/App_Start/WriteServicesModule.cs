using Ninject.Modules;

namespace Hospital.Web.App_Start
{
    public class WriteServicesModule : NinjectModule 
    {
        public override void Load()
        {
            Kernel.Bind<IHospitalWriteService>()
                .To<HospitalWriteService>()
                .InSingletonScope();
        }
    }
}