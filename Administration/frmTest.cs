using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
using EntityFramework.Extensions;

namespace Administration
{
    public partial class frmTest : Form
    {
        protected readonly TTI2Entities _context;

        DataTable DataS;
        DataColumn column;
        BindingSource BindingSrc;
        bool FormLoaded;
        Util core;
        int TNumber;
        int CNumber; 
        int PKey;
        TLADM_Styles Style = null;


        public frmTest(int TransNumber, int ColNumber, int PrimeKey)
        {
            InitializeComponent();
            core = new Util();
            TNumber = TransNumber;
            CNumber = ColNumber;
            PKey = PrimeKey;
          
            _context = new TTI2Entities();
            DataS = new DataTable();
            BindingSrc = new BindingSource();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            DataGridViewTextBoxColumn oTxtBoxA = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxB = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxC = new DataGridViewTextBoxColumn();

            DataGridViewCheckBoxColumn oChkBoxA = new DataGridViewCheckBoxColumn();

            DataColumn[] keys = new DataColumn[1];

            //0
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Col_Id";
            column.Caption = "Col Id Primary Key";
            column.DefaultValue = 0;
            keys[0] = column;
            DataS.Columns.Add(column);
            DataS.PrimaryKey = keys;
            //1
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "Col_PN";
            column.Caption = "Col Id PowerNumber";
            column.DefaultValue = 0;
        
            DataS.Columns.Add(column);

            //2
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "Col_Selected";
            column.Caption = "Selected";
            column.DefaultValue = false;
            DataS.Columns.Add(column);

            //3
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Col_Description";
            column.Caption = "Description";
            column.DefaultValue = string.Empty;
            DataS.Columns.Add(column);

            //=========================================================
            oTxtBoxA.Name = "Col0";
            oTxtBoxA.ValueType = typeof(Int32);
            oTxtBoxA.HeaderText = "Panel Key";
            oTxtBoxA.DataPropertyName = DataS.Columns[0].ColumnName;
            dataGridView1.Columns.Add(oTxtBoxA);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[0].DisplayIndex = 0;

            oTxtBoxB.Name = "Col1";
            oTxtBoxB.ValueType = typeof(Int32);
            oTxtBoxB.HeaderText = "Poer Number";
            oTxtBoxA.DataPropertyName = DataS.Columns[1].ColumnName;
            dataGridView1.Columns.Add(oTxtBoxB);
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[1].DisplayIndex = 1;

            oChkBoxA.Name = "Col2";
            oChkBoxA.ValueType = typeof(bool);
            oChkBoxA.HeaderText = "Selected";
            oChkBoxA.DataPropertyName = DataS.Columns[2].ColumnName;
            dataGridView1.Columns.Add(oChkBoxA);
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[2].DisplayIndex = 2;

            oTxtBoxC.Name = "Col3";
            oTxtBoxC.ValueType = typeof(string);
            oTxtBoxC.HeaderText = "Description";
            oTxtBoxC.Width = 350;
            oTxtBoxC.DataPropertyName = DataS.Columns[3].ColumnName;
            oTxtBoxC.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtBoxC);
            dataGridView1.Columns[3].Visible = true;
            dataGridView1.Columns[3].DisplayIndex = 3;

            BindingSrc.DataSource = DataS;
            dataGridView1.DataSource = BindingSrc;
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            IList<int> ExtrapNumbers = null;
                        
            if (TNumber == 1)
            {
                Style = _context.TLADM_Styles.Find(PKey);

                if (Style != null)
                {
                    if (CNumber == 4)
                    {
                        this.Text = "Select a size for style " + Style.Sty_Description;
                        ExtrapNumbers = core.ExtrapNumber(Style.Sty_Sizes_PN, _context.TLADM_Sizes.Count());

                        var Entities = _context.TLADM_Sizes.Where(x => !x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                        foreach (var Entity in Entities)
                        {
                            DataRow Row = DataS.NewRow();
                            Row[0] = Entity.SI_id;
                            Row[1] = Entity.SI_PowerN;
                            Row[2] = false;
                            var PNumber = ExtrapNumbers.FirstOrDefault(x => x.Equals(Entity.SI_PowerN));
                            if (PNumber > 0)
                            {
                                Row[2] = true;
                            }
                            Row[3] = Entity.SI_Display;

                            DataS.Rows.Add(Row);
                        }
                    }
                    else if (CNumber == 5)
                    {
                        this.Text = "Select a Trim for style " + Style.Sty_Description;

                        var Entities = _context.TLADM_Trims.OrderBy(x => x.TR_Description).ToList();
                        foreach (var Entity in Entities)
                        {
                            DataRow Row = DataS.NewRow();
                            Row[0] = Entity.TR_Id;
                            Row[1] = 0;
                            Row[2] = false;

                            var SelectedTrim = _context.TLADM_StyleTrim.FirstOrDefault(x => x.StyTrim_Styles_Fk == PKey && x.StyTrim_Trim_Fk == Entity.TR_Id);
                            if (SelectedTrim != null)
                            {
                                Row[2] = true;
                            }
                            Row[3] = Entity.TR_Description;

                            DataS.Rows.Add(Row);
                        }
                    }
                    else if (CNumber == 6)
                    {
                        Style = _context.TLADM_Styles.Find(PKey);
                        if (Style != null)
                        {
                            this.Text = "Select a Colour for style " + Style.Sty_Description;
                        }

                        var Entities = _context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
                        foreach (var Entity in Entities)
                        {
                            DataRow Row = DataS.NewRow();
                            Row[0] = Entity.Col_Id;
                            Row[1] = 0;
                            Row[2] = false;

                            var SelectedTrim = _context.TLADM_StyleColour.FirstOrDefault(x => x.STYCOL_Style_FK == PKey && x.STYCOL_Colour_FK == Entity.Col_Id);
                            if (SelectedTrim != null)
                            {
                                Row[2] = true;
                            }
                            Row[3] = Entity.Col_Display;

                            DataS.Rows.Add(Row);
                        }
                    }
                    else if (CNumber == 7)
                    {
                        this.Text = "Select a Lable for style " + Style.Sty_Description;

                        var Entities = _context.TLADM_Labels.Where(x => !(bool)x.Lbl_Discontinued).OrderBy(x => x.Lbl_Description).ToList();

                        foreach (var Entity in Entities)
                        {
                            DataRow Row = DataS.NewRow();

                            Row[0] = Entity.Lbl_Id;
                            Row[1] = 0;
                            Row[2] = false;

                            if (Style.Sty_Labels_FK == Entity.Lbl_Id)
                            {
                                Row[2] = true;
                            }

                            Row[3] = Entity.Lbl_Description.ToString();

                            DataS.Rows.Add(Row);

                        }
                    }
                    else if (CNumber == 15)
                    {
                        this.Text = "Select a Quality for style " + Style.Sty_Description;
                        var StyleQual = _context.TLADM_Griege.Where(x=>!(bool)x.TLGriege_Discontinued).ToList();
                        foreach (var OStyle in StyleQual)
                        {
                            DataRow Row = DataS.NewRow();

                            Row[0] = OStyle.TLGreige_Id;
                            Row[1] = 0;
                            Row[2] = false;
                            Row[3] = OStyle.TLGreige_Description;

                            DataS.Rows.Add(Row);
                        }

                        var Current = _context.TLADM_Style_Quality.Where(x=>x.SQual_Style_Fk == PKey).ToList();
                        foreach(var Record in Current)
                        {
                            var Exists = DataS.Rows.Find(Record.SQual_Griege_Fk);
                            if(Exists != null)
                            {
                                Exists[1] = Record.SQual_Pk;  
                                Exists[2] = true;
                            }
                        }
                    }
                }
                else
                {
                    using(DialogCenteringService svce = new DialogCenteringService(this))
                    {
                        MessageBox.Show("There is no Style associated with this request");
                        return;
                    }
                }
            }
            else if (TNumber == 4 && PKey > 0)
            {
                if (CNumber == 17)
                {
                    var Quality = _context.TLADM_Griege.Find(PKey);
                    if (Quality != null)
                    {
                        this.Text = "Select a Yarn for this Quality " + Quality.TLGreige_Description;
                        var Entities = _context.TLADM_Yarn.Where(x => !(bool)x.YA_Discontinued).OrderBy(x => x.YA_Description).ToList();
                        foreach (var Entity in Entities)
                        {
                            DataRow Row = DataS.NewRow();
                            Row[0] = Entity.YA_Id;
                            Row[1] = 0;
                            Row[2] = false;

                            var SelectedYarn = _context.TLADM_Greige_Yarn.FirstOrDefault(x => x.TLQual_Greige_Fk == PKey && x.TLQual_Yarn_Fk == Entity.YA_Id);
                            if (SelectedYarn != null)
                            {
                                Row[1] = SelectedYarn.TLQual_Yarn_Id;
                                Row[2] = true;
                            }
                            Row[3] = Entity.YA_Description;

                            DataS.Rows.Add(Row);
                        }
                    }
                }
            }
            FormLoaded = true;
        }

        private void frmTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                    
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && FormLoaded)
            {
                if (PKey != 0)
                {
                    if (TNumber == 1)
                    {
                        if (CNumber == 4)
                        {
                            int Result = 0;
                            Style.Sty_Sizes_PN = 0;
                            foreach (DataRow Row in DataS.Rows)
                            {
                                if ((bool)Row[2])
                                {
                                    Result += (int)Row[1];
                                }
                            }

                            Style.Sty_Sizes_PN = Result;
                        }
                        else if (CNumber == 5)
                        {
                            foreach (DataRow Row in DataS.Rows)
                            {
                                if ((bool)Row[2])
                                {
                                    var Pk = (int)Row[0];
                                    TLADM_StyleTrim StyTrim = _context.TLADM_StyleTrim.FirstOrDefault(x => x.StyTrim_Styles_Fk == PKey && x.StyTrim_Trim_Fk == Pk);
                                    if (StyTrim == null)
                                    {
                                        StyTrim = new TLADM_StyleTrim();
                                        StyTrim.StyTrim_ProdRating_FK = 0;
                                        StyTrim.StyTrim_Trim_Fk = (int)Row[0];
                                        StyTrim.StyTrim_Styles_Fk = PKey;
                                        StyTrim.StyTrim_Discontinued = false;

                                        _context.TLADM_StyleTrim.Add(StyTrim);
                                    }
                                }
                                else
                                {
                                    var Pk = (int)Row[0];
                                    TLADM_StyleTrim StyTrim = _context.TLADM_StyleTrim.FirstOrDefault(x => x.StyTrim_Styles_Fk == PKey && x.StyTrim_Trim_Fk == Pk);
                                    if (StyTrim != null)
                                    {
                                        _context.TLADM_StyleTrim.Remove(StyTrim);
                                    }
                                }
                            }
                        }
                        else if (CNumber == 6)
                        {
                            foreach (DataRow Row in DataS.Rows)
                            {
                                if ((bool)Row[2])
                                {
                                    var Pk = (int)Row[0];
                                    TLADM_StyleColour StyCol = _context.TLADM_StyleColour.FirstOrDefault(x => x.STYCOL_Style_FK == PKey && x.STYCOL_Colour_FK == Pk);
                                    if (StyCol == null)
                                    {
                                        StyCol = new TLADM_StyleColour();
                                        StyCol.STYCOL_Colour_FK = (int)Row[0];
                                        StyCol.STYCOL_Style_FK = PKey;

                                        _context.TLADM_StyleColour.Add(StyCol);
                                    }
                                }
                                else
                                {
                                    var Pk = (int)Row[0];
                                    TLADM_StyleColour StyCol = _context.TLADM_StyleColour.FirstOrDefault(x => x.STYCOL_Style_FK == PKey && x.STYCOL_Colour_FK == Pk);
                                    if (StyCol != null)
                                    {
                                        _context.TLADM_StyleColour.Remove(StyCol);
                                    }

                                }
                            }
                        }
                        else if (CNumber == 7)
                        {
                            foreach (DataRow Row in DataS.Rows)
                            {
                                if ((bool)Row[2])
                                {
                                    Style.Sty_Labels_FK = (int)Row[0];
                                    break;
                                }
                            }
                        }
                        else if (CNumber == 15)
                        {
                            foreach (DataRow Row in DataS.Rows)
                            {
                                if((bool)Row[2])
                                {
                                    int Pk = (int)Row[1];
                                    if (Pk == 0)
                                    {
                                        TLADM_Style_Quality StyQl = new TLADM_Style_Quality();
                                        StyQl.SQual_Style_Fk = PKey;
                                        StyQl.SQual_Griege_Fk = Row.Field<int>(0);
                                        _context.TLADM_Style_Quality.Add(StyQl);
                                    }
                                }
                                else
                                {
                                    TLADM_Style_Quality StyQl = new TLADM_Style_Quality();
                                    int Pk = (int)Row[1];
                                    if (Pk != 0)
                                    {
                                        StyQl = _context.TLADM_Style_Quality.Find(Pk);
                                        if (StyQl != null)
                                        {
                                            _context.TLADM_Style_Quality.Remove(StyQl);
                                        }
                                    }
                                }
                            }
                        }
                        else if (CNumber == 20)
                        {
                            foreach (DataRow Row in DataS.Rows)
                            {
                                if ((bool)Row[2])
                                {
                                    int Pk = (int)Row[0];
                                    TLADM_StyAssoc StyAss = _context.TLADM_StyAssoc.FirstOrDefault(x => x.StyAssoc_StyPk == PKey && x.StyAssoc_StyOther == Pk);
                                    if (StyAss == null)
                                    {
                                        StyAss = new TLADM_StyAssoc();
                                        StyAss.StyAssoc_StyOther = Pk;
                                        StyAss.StyAssoc_StyPk = PKey;

                                        _context.TLADM_StyAssoc.Add(StyAss);
                                    }
                                }
                                else
                                {
                                    if ((int)Row[1] != 0)
                                    {
                                        var Pk = (int)Row[1];
                                        var StyAss = _context.TLADM_StyAssoc.Find(Pk);
                                        if (StyAss != null)
                                        {
                                            _context.TLADM_StyAssoc.Remove(StyAss);
                                        }
                                    }
                                }
                            }
                        }                       
                    }
                    else if (TNumber == 4 && CNumber == 17)
                    {
                        foreach (DataRow Row in DataS.Rows)
                        {
                            if ((bool)Row[2])
                            {
                                int Pk = (int)Row[0];
                                TLADM_Greige_Yarn GYarn = _context.TLADM_Greige_Yarn.Where(x => x.TLQual_Yarn_Fk == Pk && x.TLQual_Greige_Fk == PKey).FirstOrDefault();
                                if (GYarn == null)
                                {
                                    GYarn = new TLADM_Greige_Yarn();
                                    GYarn.TLQual_Greige_Fk = PKey;
                                    GYarn.TLQual_Yarn_Fk = Pk;

                                    _context.TLADM_Greige_Yarn.Add(GYarn);
                                }
                            }
                            else if ((int)Row[1] != 0)
                            {
                                var Pk = (int)Row[1];
                                var StyAss = _context.TLADM_Greige_Yarn.Find(Pk);
                                if (StyAss != null)
                                {
                                    _context.TLADM_Greige_Yarn.Remove(StyAss);
                                }
                            }
                            
                        }
                    }
                    try
                    {
                        _context.SaveChanges();
                        using (DialogCenteringService svcs = new DialogCenteringService(this))
                        {
                            MessageBox.Show("Successfully saved to database");
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        this.Close();
                    }
                }
            }
        }
    }
}
