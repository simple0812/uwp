using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpDemo.Libs
{
    public class DependencyProperty
    {
        private static int globalIndex = 0;
        public static Dictionary<object, DependencyProperty> RegisteredDps = new Dictionary<object, DependencyProperty>();
        public string Name;
        public object Value;
        public object HashCode;
        public int Index;
        //同一个DP，要想支持不同的默认值，那么内部就要维护一个对应不同DependencyObjectType的一个List，可以根据传入的DependencyObject的类型来读取它对应的默认值。
        private List<PorpertypeMetadata> _metadataMap = new List<PorpertypeMetadata>(); 
        private PorpertypeMetadata _defaultMetadata;

        private DependencyProperty(string name, Type propertyName, Type ownerType, object defaultValue)
        {
            this.Name = name;
            this.Value = defaultValue;
            this.HashCode = name.GetHashCode() ^ ownerType.GetHashCode();

            PorpertypeMetadata metadata = new PorpertypeMetadata(defaultValue) {Type = ownerType};
            _metadataMap.Add(metadata);
            _defaultMetadata = metadata;
        }

        public static DependencyProperty Register(string name, Type properType, Type ownerType, object defaultValue)
        {
            DependencyProperty dp = new DependencyProperty(name, properType, ownerType, defaultValue)
            {
                Index = ++ globalIndex
            };
            RegisteredDps.Add(dp.HashCode, dp);
            return dp;
        }

        public void OverrideMetadata(Type forType, PorpertypeMetadata metadata)
        {
            metadata.Type = forType;
            _metadataMap.Add(metadata);
        }

        public PorpertypeMetadata GetMetadata(Type type)
        {
            PorpertypeMetadata metadata = _metadataMap.FirstOrDefault((i) => i.Type == type) ?? _defaultMetadata;

            return metadata;
        }

    }
}
