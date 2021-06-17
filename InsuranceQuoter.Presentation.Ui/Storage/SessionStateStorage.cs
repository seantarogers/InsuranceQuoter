//namespace InsuranceQuoter.Presentation.Ui.Storage
//{
//    using System.Threading.Tasks;
//    using Blazored.SessionStorage;
//    using Fluxor.Persist.Storage;

//    public class SessionStateStorage : IObjectStateStorage
//    {
//        public SessionStateStorage(ISessionStorageService sessionStorage)
//        {
//            SessionStorage = sessionStorage;
//        }

//        public ValueTask<string> GetStateJsonAsync(string stateName) =>
//            SessionStorage.GetItemAsStringAsync(stateName);

//        public ValueTask StoreStateJsonAsync(string statename, string json) =>
//            SessionStorage.SetItemAsStringAsync(statename, json);

//        private ISessionStorageService SessionStorage { get; set; }

//        public ValueTask<object> GetStateAsync(string stateName) =>
//            SessionStorage.GetItemAsync<object>(stateName);

//        public ValueTask StoreStateAsync(string statename, object state) =>
//            SessionStorage.SetItemAsStringAsync(statename, state);
//    }
//}