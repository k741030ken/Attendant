using System.Xml.Serialization;

namespace Office.Word
{
    public class InputMessageField
    {
        /// <summary>
        /// 變數名稱
        /// </summary>
        private string _VariableID = "";

        /// <summary>
        /// 變數值
        /// </summary>
        private string _VariableValue = "";

        /// <summary>
        /// 是否為多筆，0為只有1筆。多筆資料由1開始
        /// </summary>
        private string _Multi = "0";

        [XmlAttribute("VariableID")]
        public string VariableID
        {
            get { return _VariableID; }
            set { _VariableID = value; }
        }

        [XmlAttribute("VariableValue")]
        public string VariableValue
        {
            get { return _VariableValue; }
            set { _VariableValue = value; }
        }

        [XmlAttribute("Multi")]
        public string Multi
        {
            get { return _Multi; }
            set { _Multi = value; }
        }
    }
}