using Autofac;
using SimApi.Data.Repository;
using SimApi.Data.Uow;
using SimApi.Operation;

namespace SimApi.Service;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserLogService>().As<IUserLogService>().InstancePerLifetimeScope();
        builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();
        builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
        builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
        builder.RegisterType<TransactionService>().As<ITransactionService>().InstancePerLifetimeScope();
        builder.RegisterType<TransactionReportService>().As<ITransactionReportService>().InstancePerLifetimeScope();

        builder.RegisterType<DapperAccountService>().As<IDapperAccountService>().InstancePerLifetimeScope();
        builder.RegisterType<DapperCategoryService>().As<IDapperCategoryService>().InstancePerLifetimeScope();

        builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();


    }
}
