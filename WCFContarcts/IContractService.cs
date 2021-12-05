using System.ServiceModel;

namespace WCFContarcts
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IContractService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IContractService
    {
        [OperationContract]
        string LoadBase();

        [OperationContract]
        void SaveBase(string str);
    }
    
}
