using System.Collections.Generic;
using System.Linq;

namespace DpDemo.Libs
{
    public class DependencyObject
    {
        //就是把所有修改过的DP都保存在EffectiveValueEntry里，这样，就可以达到只保存修改的属性，未修改过的属性仍然读取DP的默认值，优化了属性的储存。
        private List<EffectiveValueEntry> _effectiveValueEntries = new List<EffectiveValueEntry>();
         

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof (string), typeof (DependencyObject), string.Empty);

        public object GetValue(DependencyProperty dp)
        {
            EffectiveValueEntry effectiveValue =
                _effectiveValueEntries.FirstOrDefault((i) => i.PropertyIndex == dp.Index);

            if (effectiveValue != null && effectiveValue.PropertyIndex != 0)
            {
                return effectiveValue.Value;
            } 

            PorpertypeMetadata metadata = DependencyProperty.RegisteredDps[dp.HashCode].GetMetadata(this.GetType());
            return metadata.Value;
        }

        public void SetValue(DependencyProperty dp, object value)
        {
            EffectiveValueEntry effectiveValue = _effectiveValueEntries.FirstOrDefault((i) => i.PropertyIndex == dp.Index);
            if (effectiveValue != null && effectiveValue.PropertyIndex != 0)
                effectiveValue.Value = value;
            else
            {
                effectiveValue = new EffectiveValueEntry() {PropertyIndex = dp.Index, Value = value};
                _effectiveValueEntries.Add(effectiveValue);
                //DependencyProperty.RegisteredDps[dp.HashCode].Value = value;
            }
        }

        public string Name
        {
            get { return (string) GetValue(NameProperty); }
            set { SetValue(NameProperty, value);}
        }
    }
}