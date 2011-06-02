namespace TSS.Models 
{
    using System;
    public class ChartData
    {
        public ChartData() { }
        /// <summary>
        /// 实验ID
        /// </summary>
        public Guid ExperimentId { get; set; }
        /// <summary>
        /// 实验结果
        /// </summary>
        public int ExpResult { get; set; }
        /// <summary>
        /// 实验时间
        /// </summary>
        public DateTime ExpDate { get; set; }
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid EquipmentId { get; set; }
        /// <summary>
        /// 实验报告模板坐标
        /// </summary>
        public string Coord { get; set; }
        /// <summary>
        /// 实验数据
        /// </summary>
        public decimal? CoordValue { get; set; }
    }
}