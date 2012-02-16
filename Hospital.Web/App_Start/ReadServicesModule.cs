using Hospital.ReadModel.Web;
using Ninject.Modules;

namespace Hospital.Web.App_Start
{
    public class ReadServicesModule : NinjectModule 
    {
        public override void Load()
        {
            Kernel.Bind<IHospitalReadService>()
                .To<HospitalReadService>()
                .InSingletonScope();
        }
    }
}