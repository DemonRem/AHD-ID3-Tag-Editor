using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace MTC
{
    public class MTCTabPagesEditor : CollectionEditor
    {
        public MTCTabPagesEditor(Type type)
            : base(type)
        {
        }
        protected override string GetDisplayText(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            PropertyDescriptor defaultProperty = TypeDescriptor.GetDefaultProperty(base.CollectionType);
            string text;
            if (defaultProperty != null && defaultProperty.PropertyType == typeof(string))
            {
                text = (string)defaultProperty.GetValue(value);
                if (text != null && text.Length > 0)
                {
                    return text;
                }
            }
            text = TypeDescriptor.GetConverter(value).ConvertToString(value);
            if (text == null || text.Length == 0)
            {
                text = value.GetType().Name;
            }
            return text;
        }
        protected override object SetItems(object editValue, object[] value)
        {
            MTCTabPagesCollection original = editValue as MTCTabPagesCollection;

            original.Clear();
            foreach (MTCTabPage entry in value)
            {
                original.Add(entry.Clone());
            }
            return original;
        }
        protected override object[] GetItems(object editValue)
        {
            if (editValue == null)
            {
                return new object[0];
            }
            MTCTabPagesCollection dictionary = editValue as MTCTabPagesCollection;
            if (dictionary == null)
            {
                throw new ArgumentNullException("editValue");
            }
            object[] objArray = new object[dictionary.Count];
            int num = 0;
            foreach (MTCTabPage entry in dictionary)
            {
                MTCTabPage entry2 = entry.Clone();
                objArray[num++] = entry2;
            }
            return objArray;
        }
        protected override object CreateInstance(Type itemType)
        {
            if (itemType == typeof(MTCTabPage))
            { return new MTCTabPage(); }
            return base.CreateInstance(itemType);
        }
        protected override Type CreateCollectionItemType()
        {
            return typeof(MTCTabPage);
        }
    }
}
