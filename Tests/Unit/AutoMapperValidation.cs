using AutoMapper;

public class AutoMapperValidation(IMapper mapper)
{
    //[Fact]
    //public void Validate()
    //{
    //    var mapperConfig = new MapperConfiguration(cfg =>
    //    {
    //        // NOTE global mapper should be first, since it has the prefix configurations
    //        var mapperTypes = new[] {
    //            typeof(GlobalMapper), typeof(CurrencyRepositoryMapper), typeof(PaymentRepositoryMapper), typeof(ProgramRepositoryMapper), typeof(ContractRepositoryMapper),
    //            typeof(InvoiceRepositoryMapper), typeof(InvoiceLineDetailRepositoryMapper)
    //        };
    //        cfg.AddMaps(mapperTypes);
    //    });

    //    mapperConfig.AssertConfigurationIsValid();
    //}

    [Fact]
    public void Contract_Mapping()
    {
        var obj = FakeData.Contracts[0];
        var entity = mapper.Map<Vsd_Contract>(obj);

        Assert.Equal((int)obj.ContractType, (int)entity.Vsd_Type);
        Assert.Equal((int)obj.StateCode, (int)entity.StateCode);
        Assert.Equal((int)obj.StatusCode, (int)entity.StatusCode);
        Assert.Equal(obj.Id, entity.Id);
    }

    [Fact]
    public void Program_Mapping()
    {
        var obj = FakeData.Programs[0];
        var entity = mapper.Map<Vsd_Program>(obj);

        Assert.Equal((int)obj.StateCode, (int)entity.StateCode);
        Assert.Equal((int)obj.StatusCode, (int)entity.StatusCode);
        Assert.Equal(obj.Id, entity.Id);
        // TODO get this mapping working
        Assert.Equal(obj.ContractName, entity.Vsd_ContractIdName);
        Assert.Equal(obj.OwnerId, entity.OwnerId.Id);
        Assert.Equal(obj.ProvinceState, entity.Vsd_ProvinceState);
        Assert.Equal(obj.BudgetProposalSignatureDate, entity.Vsd_BudgetProposalSignaturedAte);
        Assert.Equal(obj.Name, entity.Vsd_Name);
        Assert.Equal(obj.CpuSubtotal, entity.Vsd_Cpu_SubtotalComponentValue.Value);
    }
}
