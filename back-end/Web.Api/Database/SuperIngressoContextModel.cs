using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Web.Api.Database;

namespace Web.Api.Database
{

    [DbContext(typeof(SuperIngressoContext))]
    public partial class SuperIngressoContextModel : RuntimeModel
    {
        private static SuperIngressoContextModel _instance;

        static SuperIngressoContextModel()
        {
            var model = new SuperIngressoContextModel();
            model.Initialize();
            model.Customize();
            _instance = model;
        }

        public static IModel Instance => _instance;

        partial void Initialize();
        partial void Customize();
    }
}
