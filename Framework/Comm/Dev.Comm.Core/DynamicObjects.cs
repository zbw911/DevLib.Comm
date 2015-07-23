using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace Dev.Comm
{


    public class DynamicObjects : DynamicObject
    {
        private IDictionary<string, object> _dictionary { get; set; }
        private List<object> _list { get; set; }

        public DynamicObjects(object parse)
        {
            if (parse is IDictionary<string, object>)
                _dictionary = (IDictionary<string, object>)parse;
            else if (parse is IEnumerable)
                _list = new List<object>((IEnumerable<object>)parse);
            else
                _dictionary = new ValueDictionary(parse);
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _dictionary.Keys.ToList();
        }

        public override bool TryGetIndex(GetIndexBinder binder, Object[] indexes, out Object result)
        {
            var index = indexes[0];
            if (index is int)
            {
                result = _list[(int)index];
            }
            else
            {
                result = _dictionary[(string)index];
            }
            if (result is IDictionary<string, object>)
                result = new DynamicObjects(result as IDictionary<string, object>);
            else if (result is IEnumerable<object>)
                result = new DynamicObjects(result as IEnumerable<object>);

            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_dictionary.TryGetValue(binder.Name, out result) == false)
                if (_dictionary.TryGetValue(binder.Name.ToLower(), out result) == false)
                    return false;// throw new Exception("property not found " + binder.Name);

            if (result is IDictionary<string, object>)
            {
                result = new DynamicObjects(result as IDictionary<string, object>);
            }
            else if (result is IEnumerable<object>)
            {
                List<object> list = new List<object>();
                foreach (object item in (List<object>)result)
                {
                    if (item is IDictionary<string, object>)
                        list.Add(new DynamicObjects(item as IDictionary<string, object>));
                    else
                        list.Add(item);
                }
                result = list;
            }

            return _dictionary.ContainsKey(binder.Name);
        }
    }


    /// <summary>
    /// 这个类复制自  System.Web.RouteDictionary
    /// </summary>
    internal class ValueDictionary : IDictionary<string, object>
    {
        private Dictionary<string, object> _dictionary;

        public ValueDictionary(object values)
        {
            _dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            AddValues(values);
        }

        public ValueDictionary(IDictionary<string, object> dictionary)
        {
            _dictionary = new Dictionary<string, object>(dictionary, StringComparer.OrdinalIgnoreCase);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        public int Count
        {
            get
            {
                return _dictionary.Count;
            }
        }

        public Dictionary<string, object>.KeyCollection Keys
        {
            get
            {
                return _dictionary.Keys;
            }
        }

        public Dictionary<string, object>.ValueCollection Values
        {
            get
            {
                return _dictionary.Values;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        public object this[string key]
        {
            get
            {
                object value;
                TryGetValue(key, out value);
                return value;
            }
            set
            {
                _dictionary[key] = value;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        public void Add(string key, object value)
        {
            _dictionary.Add(key, value);
        }

        private void AddValues(object values)
        {
            if (values != null)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(values);
                foreach (PropertyDescriptor prop in props)
                {
                    object val = prop.GetValue(values);
                    Add(prop.Name, val);
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        public void Clear()
        {
            _dictionary.Clear();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool ContainsValue(object value)
        {
            return _dictionary.ContainsValue(value);
        }

        public Dictionary<string, object>.Enumerator GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        public bool Remove(string key)
        {
            return _dictionary.Remove(key);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        public bool TryGetValue(string key, out object value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        #region IDictionary<string,object> Members
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        ICollection<string> IDictionary<string, object>.Keys
        {
            get
            {
                return _dictionary.Keys;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        ICollection<object> IDictionary<string, object>.Values
        {
            get
            {
                return _dictionary.Values;
            }
        }
        #endregion 

        #region ICollection<KeyValuePair<string,object>> Members 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
        {
            ((ICollection<KeyValuePair<string, object>>)_dictionary).Add(item);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)_dictionary).Contains(item);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, object>>)_dictionary).CopyTo(array, arrayIndex);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        bool ICollection<KeyValuePair<string, object>>.IsReadOnly
        {
            get
            {
                return ((ICollection<KeyValuePair<string, object>>)_dictionary).IsReadOnly;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)_dictionary).Remove(item);
        }
        #endregion

        #region IEnumerable<KeyValuePair<string,object>> Members
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion 

        #region IEnumerable Members 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}




