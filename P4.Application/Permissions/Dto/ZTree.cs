using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Permissions.Dto
{
    public class ZTree
    {
        //{ id: 1, pId: 0, name: "随意勾选 1", open: true },
        //    { id: 11, pId: 1, name: "随意勾选 1-1" },
        //    { id: 12, pId: 1, name: "随意勾选 1-2", open: true },
        //    { id: 121, pId: 12, name: "随意勾选 1-2-1" },
        //    { id: 122, pId: 12, name: "随意勾选 1-2-2" },
        //{ id: 2, pId: 0, name: "禁止勾选 2", open: true, doCheck: false },

        /// <summary>
        /// 节点ID
        /// </summary>
        private string _id;
        /// <summary>
        /// 父节点ID
        /// </summary>
        private string _pId;
        /// <summary>
        /// 节点名字
        /// </summary>
        private string _name;
        /// <summary>
        /// 节点是否打开
        /// </summary>
        private bool _open;
        /// <summary>
        /// 禁止勾选，checkbox正常状态 点击无反应
        /// </summary>
        private bool _doCheck;
        /// <summary>
        /// 节点被勾选
        /// </summary>
        private bool _checked;
        /// <summary>
        /// 节点禁用 checkbox 为灰色状态
        /// </summary>
        private bool _chkDisabled;
      
        /// <summary>
        /// 节点不显示 checkbox
        /// </summary>
        private bool _nocheck;

        

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }


        public string pId
        {
            get { return _pId; }
            set { _pId = value; }
        }


        public string name
        {
            get { return _name; }
            set { _name = value; }
        }


        public bool open
        {
            get { return _open; }
            set { _open = value; }
        }


        public bool doCheck
        {
            get { return _doCheck; }
            set { _doCheck = value; }
        }


        public bool check
        {
            get { return _checked; }
            set { _checked = value; }
        }

        public bool chkDisabled
        {
            get { return _chkDisabled; }
            set { _chkDisabled = value; }
        }

        public bool nocheck
        {
            get { return _nocheck; }
            set { _nocheck = value; }
        }

        //{"id":"tenant1","pId":null,"name":"administrator","checked":true,"doCheck":true,"open":true,"chkDisabled":false,"children":
    }
}
