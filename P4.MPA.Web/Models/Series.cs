using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    public class Series
    {

        /// <summary>
        /// sereis序列组id
        /// </summary>
        public int id
        {
            get;
            set;
        }

        /// <summary>
        /// series序列组名称
        /// </summary>
        public string name
        {
            get;
            set;
        }

        /// <summary>
        /// series序列组呈现图表类型(line、column、bar等)
        /// </summary>
        public string type
        {
            get;
            set;
        }
        /// <summary>
        /// 柱状的宽度
        /// </summary>
        //public string barGap
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// series序列组的数据为数据类型数组
        /// </summary>
        public List<decimal> data
        {
            get;
            set;
        }

    }

    public class EcharModel
    {
        public List<string> categoryList;
        public List<string> legendList;
        public Series seriesObj;
        public Series seriesObj2;
        public Series seriesObj3;
        public Series seriesObj4;
        public Series seriesObj5;
        public Series seriesObj6;

        //动态线
        public List<Series> series;

    }
}