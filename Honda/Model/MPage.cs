using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model
{
    [Serializable]
    public class MPage
    {
        private string pageName;

        public string _pageName
        {
            get { return pageName; }
            set
            {
                if (pageName != value)
                {
                    pageName = value;
                }
            }
        }

        public MPage(string name)
        {
            _pageName = name;
        }


        /// <summary>
        /// 正常状态下的图片URI
        /// </summary>
        private readonly string strNormalImaUri = "/Assets/EvaluationOfTour/page_nav1.png";

        public string ImgNormalUri
        {
            get { return strNormalImaUri; }
        }


        /// <summary>
        /// 选中状态下的图片URI
        /// </summary>
        private readonly string strPitchOnImaUri = "/Assets/EvaluationOfTour/page_nav1_1.png";

        public string ImgSelectedUri
        {
            get { return strPitchOnImaUri; }
        }
    }
}