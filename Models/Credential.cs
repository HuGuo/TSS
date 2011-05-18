using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TSS.Models
{
    public class Credential
    {
        public Credential() { }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 持证人姓名
        /// </summary>
        public string NameCN { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        public string Company_ID { get; set; }
        /// <summary>
        /// 单位部门班组 名称
        /// </summary>
        public string Company_Name { get; set; }

        /// <summary>
        /// 作业种类
        /// </summary>
        public string Job_Type { get; set; }

        /// <summary>
        /// 资格项目
        /// </summary>
        public string Job_PRJ { get; set; }

        /// <summary>
        /// 培训单位
        /// </summary>
        [MaxLength(250)]
        public string Training_Unit { get; set; }

        /// <summary>
        /// 取证时间
        /// </summary>
        [Column(TypeName = "smalldatetime")]
        public DateTime GetDate { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        [Column(TypeName = "smalldatetime")]
        public DateTime Validdate { get; set; }
        /// <summary>
        /// 证书编号
        /// </summary>
        [Column("C_NUMBER"), MaxLength(250)]
        public string Number { get; set; }

        /// <summary>
        /// 效果图路径
        /// </summary>
        public string PNGName { get; set; }

        [MaxLength(250)]
        public string Remark { get; set; }

        public string SP_CODE { get; set; }
        public int IsDel { get; set; }
    }
}
