internal class KrxData
{

    public class Rootobject
    {
        public Outblock_1[] OutBlock_1 { get; set; }
        public string CURRENT_DATETIME { get; set; }
    }

    public class Outblock_1
    {
        public string ISU_CD { get; set; }
        public string ISU_SRT_CD { get; set; }
        public string ISU_NM { get; set; }
        public string ISU_ABBRV { get; set; }
        public string ISU_ENG_NM { get; set; }
        public string LIST_DD { get; set; }
        public string MKT_TP_NM { get; set; }
        public string SECUGRP_NM { get; set; }
        public string SECT_TP_NM { get; set; }
        public string KIND_STKCERT_TP_NM { get; set; }
        public string PARVAL { get; set; }
        public string LIST_SHRS { get; set; }
    }

}