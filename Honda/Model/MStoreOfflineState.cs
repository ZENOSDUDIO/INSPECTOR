using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model
{
    [Serializable]
    public class MStoreOfflineState
    {
        private Dictionary<string, bool> _store_need_load_offline_data_dic;

        public IList<MStore> _LstStore
        {
            set
            {
                foreach (var item in value)
                {
                    if (!_store_need_load_offline_data_dic.ContainsKey(item.shopId))
                        _store_need_load_offline_data_dic.Add(item.shopId, false);
                }
            }
        }

        public MStoreOfflineState()
        {
            _store_need_load_offline_data_dic = new Dictionary<string, bool>();
        }

        public bool GetStoreNeedLoadOfflineDataState(MStore store)
        {
            if (!_store_need_load_offline_data_dic.ContainsKey(store.shopId)) return false;

            return _store_need_load_offline_data_dic[store.shopId];
        }

        public void SetStoreNeedLoadOfflineDataState(MStore store, bool state)
        {
            if (!_store_need_load_offline_data_dic.ContainsKey(store.shopId))
                _store_need_load_offline_data_dic.Add(store.shopId, state);
            _store_need_load_offline_data_dic[store.shopId] = state;
        }
    }
}