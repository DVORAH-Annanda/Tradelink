using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace TTI2_WF
{
    public partial class frmTLADMGardProp : Form
    {
        public int TotalPN;
        // bool nonNumeric;
        Util core = new Util();
        DataGridViewCheckBoxColumn selectedProperty;
        IList<int> propertyValues;
        IList<TLADM_Sizes> _Sizes = null;
        int _Trims = 0;
        int _StyleTrims = 0;
        int _CustFK;
        int _TotalPN = 0;
        int _Col = 0;
        public int[] _TrimKeys = { 0, 0, 0};

        Administration.AdminQueryParameters Parms;
        Administration.AdminRepository repo;

        bool _DoCheck = false;

        public frmTLADMGardProp(int col, int powerN)
        {
            InitializeComponent();
          
            _Col = col;
            _TotalPN = powerN;
            SetUp(col, powerN);
        }

        public frmTLADMGardProp(int col, int powerN, bool DoCheck)
        {
            InitializeComponent();
          
            _Col = col;
            _TotalPN = powerN;
            _DoCheck = DoCheck;
            SetUp(col, powerN);
        }

        public frmTLADMGardProp(int col, int powerN, int CustFK)
        {
            InitializeComponent();
            _CustFK = CustFK;
            _Col = col;
            _TotalPN = powerN;
            SetUp(col, powerN);

        }
        public frmTLADMGardProp(int col, int powerN, IList<TLADM_Sizes> sizes )
        {
            InitializeComponent();
            _Sizes = sizes;
            _Col = col;
            _TotalPN = powerN;
            SetUp(col, powerN);
        }

        public frmTLADMGardProp(int col, int[] TrimKeys, bool DoCheck)
        {
            InitializeComponent();
            //===========================================================
            // _TrimKeys  [0] = Style Pk
            //            [1] = Trims Pk
                        
            //============================================================
            _Col = col;             // What part of the program to invoke
            _DoCheck = DoCheck;
            _TrimKeys = TrimKeys;
            SetUp(col, TrimKeys[0]);
        }

        private void SetUp(int col, int powernumber)
        {
            Util core = new Util();
            DataGridViewTextBoxColumn descriptiona = new DataGridViewTextBoxColumn(); //0

            comboColours.Visible = false;

            if (col == 1005)
            {
                descriptiona.HeaderText = "Size Selection";
                this.Text = "Please select the size's appropriate to this style";
            }
            else if (col == 1006)
            {
                descriptiona.HeaderText = "Colours Selection";
                this.Text = "Please select the colour's appropriate to this style";
            }
            else if (col == 1007)
            {
                descriptiona.HeaderText = "Ribbing Selection";
                this.Text = "Please select the ribbing's appropriate to this style";
            }
            else if (col == 1008)
            {
                descriptiona.HeaderText = "Trims Selection";
                this.Text = "Please select the trim's appropriate to this style";
            }
            else if (col == 1009)
            {
                descriptiona.HeaderText = "Fabric Product Selection";
                this.Text = "Please select the fabric product appropriate to this style";
            }
            else if (col == 1010)
            {
                descriptiona.HeaderText = "Fabric Product Group Selection";
                this.Text = "Please select the fabric product groups appropriate to this style";
            }
            else if (col == 1012)
            {
                descriptiona.HeaderText = "Fabric Product Group Selection";
                this.Text = "Please select the fabric product groups appropriate to this style";
            }
            else if (col == 1013)
            {
                descriptiona.HeaderText = "Panel Product Group Selection";
                this.Text = "Please select the panel appropriate to this style";
            }
            else if (col == 1014)
            {
                descriptiona.HeaderText = "Label Selection";
                this.Text = "Please select the Lable for this style";
            }
            else if (col == 1015)
            {
                descriptiona.HeaderText = "Label Selection";
                this.Text = "Please select the Lable for this style";
            }
            else if (col == 2006)
            {
                descriptiona.HeaderText = "Colour Thread selection";
                this.Text = "Please update the associated colour thread code";
            }
            else if (col == 4005)
            {
                descriptiona.HeaderText = "Yarn Selection";
                this.Text = "Please select the yarn appropriate to this garment";
            }
            else if (col == 4006)
            {
                descriptiona.HeaderText = "Yarn Tex Selection";
                this.Text = "Please select the yarn tex appropriate to this Greige";
            }
            else if (col == 4007)
            {
                descriptiona.HeaderText = "Fabric weight selection";
                this.Text = "Please select the fabric weight appropriate to this garment";
            }

            else if (col == 4020)
            {
                descriptiona.HeaderText = "Colour Item Selection";
                this.Text = "Please select the Colour appropriate to this Item";
            }
            else if (col == 5000)
            {
                descriptiona.HeaderText = "Department selection";
                this.Text = "Please select the department as appropriate to this non stock item";
            }
            else if (col == 6000)
            {
                descriptiona.HeaderText = "Maintenance selection";
                this.Text = "Please select the maintenance as appropriate machine ";
            }

            else if (col == 7000)
            {
                descriptiona.HeaderText = "Label selection";
                this.Text = "Please select the label as appropriate to this company ";
            }
            else if (col == 17011)
            {
                descriptiona.HeaderText = "Fabric Attribute selection";
                this.Text = "Please select the fabric attribute as appropriate to this panel ";

            }

            this.dataGridViewxx.AllowUserToAddRows = false;
            this.dataGridViewxx.AutoGenerateColumns = false;

            if (col == 4020)
            {
                Parms = new Administration.AdminQueryParameters();
                repo = new Administration.AdminRepository();
                this.comboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
                
                comboColours.Visible = true;
                this.dataGridViewxx.Visible = false;

            }
            else if (col != 2006)
            {
                selectedProperty = new DataGridViewCheckBoxColumn();//1
                selectedProperty.HeaderText = "Selected?";
                selectedProperty.DataPropertyName = "SelectedProperty";

                DataGridViewTextBoxColumn powerNumber = new DataGridViewTextBoxColumn(); // 2
                powerNumber.HeaderText = "Power Number";
                powerNumber.DataPropertyName = "Power_Number";
                powerNumber.Visible = false;
              

                this.dataGridViewxx.Columns.Add(descriptiona);    //0
                this.dataGridViewxx.Columns.Add(selectedProperty); //1
                this.dataGridViewxx.Columns.Add(powerNumber);      //2

                dataGridViewxx.AllowUserToOrderColumns = false;

            }
            else
            {
                DataGridViewTextBoxColumn PrimaryKey = new DataGridViewTextBoxColumn(); // 0
                PrimaryKey.HeaderText = "Primary Pk";
                PrimaryKey.DataPropertyName = "Primary_Pk";
                PrimaryKey.Visible = false;

                DataGridViewTextBoxColumn ColourPk = new DataGridViewTextBoxColumn(); // 1
                ColourPk.HeaderText = "Colour Pk";
                ColourPk.DataPropertyName = "Colour_Pk";
                ColourPk.Visible = false;
            
                DataGridViewTextBoxColumn ShortCode = new DataGridViewTextBoxColumn();  // 2
                ShortCode.HeaderText = "Short Code";
                ShortCode.DataPropertyName = "Short_Code";

                DataGridViewTextBoxColumn Description = new DataGridViewTextBoxColumn(); // 3
                Description.HeaderText = "Description";
                ShortCode.DataPropertyName = "Description";

                this.dataGridViewxx.AllowUserToAddRows = true;

                this.dataGridViewxx.Columns.Add(PrimaryKey);    //0
                this.dataGridViewxx.Columns.Add(ColourPk);      //1
                this.dataGridViewxx.Columns.Add(ShortCode);     //2
                this.dataGridViewxx.Columns.Add(Description);   //3
            }

          

          
            using (var context = new TTI2Entities())
            {
                try
                {

                    if (col == 1005)
                    {
                        IList<TLADM_Sizes> ExistingData;

                        if (_Sizes == null)
                           ExistingData = context.TLADM_Sizes.Where(x => !(bool)x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                        else
                            ExistingData = _Sizes.Where(x => !(bool)x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                                                                      
                        propertyValues = core.ExtrapNumber(powernumber, context.TLADM_Sizes.Count());

                        foreach (var row in ExistingData)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = row.SI_Description;

                            if (propertyValues.Contains((int)row.SI_PowerN))
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = true;
                            }
                            else
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = false;
                            }

                            this.dataGridViewxx.Rows[index].Cells[2].Value = row.SI_PowerN;
                        }
                    }
                    else if (col == 1006)
                    {
                        var existingData = context.TLADM_Colours
                                                       .OrderBy(x => x.Col_Display).ToList();

                      

                        // propertyValues = core.ExtrapNumber(powernumber, existingData.Count());

                        foreach (var row in existingData)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = row.Col_Display;

                            var StyleCol = context.TLADM_StyleColour.Where(x => x.STYCOL_Style_FK == powernumber && x.STYCOL_Colour_FK == row.Col_Id).FirstOrDefault();
                            if(StyleCol != null)
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = true;
                            }
                            else
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = false;
                            }

                            this.dataGridViewxx.Rows[index].Cells[2].Value = row.Col_Id;
                            
                        }
                    }
                    else if (col == 1007)
                    {
                        var Existing = context.TLADM_Trims.Where(x=>!(bool)x.TR_Discontinued).OrderBy(x => x.TR_Description).ToList();
                        foreach (var Row in Existing)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = Row.TR_Description;
                            this.dataGridViewxx.Rows[index].Cells[1].Value = false;
                            if(_TrimKeys[1] == Row.TR_Id)   
                            {
                                 this.dataGridViewxx.Rows[index].Cells[1].Value = true;
                            }
                            this.dataGridViewxx.Rows[index].Cells[2].Value = Row.TR_Id;
                        }
                    }
                    else if (col == 2006)
                    {
                        var ExistingData = context.TLADM_AuxColours.Where(x => x.AuxCol_Colour_Fk ==  powernumber).ToList();
                        if (ExistingData.Count == 0)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = null;
                            this.dataGridViewxx.Rows[index].Cells[1].Value = TotalPN;
                        }

                        foreach (var row in ExistingData)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = row.AuxCol_Id ;
                            this.dataGridViewxx.Rows[index].Cells[1].Value = row.AuxCol_Colour_Fk;
                            this.dataGridViewxx.Rows[index].Cells[2].Value = row.AuxCol_FinishedCode;
                            this.dataGridViewxx.Rows[index].Cells[3].Value = row.AuxCol_Description;
                        }
                    }
                    else if (col == 2007)
                    {
                        var existingData = context.TLADM_AuxColours
                                                        .OrderBy(x => x.AuxCol_Id).ToList();
                    }
                   else if (col == 1008)
                   {
                        var existingData = context.TLADM_Griege
                                                       .OrderBy(x => x.TLGreige_Id).ToList();

                        propertyValues = core.ExtrapNumber(powernumber, existingData.Count());

                        foreach (var row in existingData)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = row.TLGreige_Description;

                            if (propertyValues.Contains((int)row.TLGreige_PowerN))
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = true;
                            }
                            else
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = false;
                            }

                            this.dataGridViewxx.Rows[index].Cells[2].Value = row.TLGreige_PowerN;
                        }
                    }
                    else if (col == 1009)
                    {
                        var existingData = context.TLADM_FabricWeight
                                                       .OrderBy(x => x.FWW_Id).ToList();


                        propertyValues = core.ExtrapNumber(powernumber, existingData.Count());

                        foreach (var row in existingData)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = row.FWW_Description;

                            if (propertyValues.Contains((int)row.FWW_PowerN))
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = true;
                            }
                            else
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = false;
                            }

                            this.dataGridViewxx.Rows[index].Cells[2].Value = row.FWW_PowerN;
                        }
                    }
                    else if (col == 1010)
                    {
                        var existingData = context.TLADM_FabricProduct
                                                       .OrderBy(x => x.FP_Id).ToList();


                        propertyValues = core.ExtrapNumber(powernumber, existingData.Count());

                        foreach (var row in existingData)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = row.FP_Description;

                            if (propertyValues.Contains((int)row.FP_PowerN))
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = true;
                            }
                            else
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = false;
                            }

                            this.dataGridViewxx.Rows[index].Cells[2].Value = row.FP_PowerN;
                        }
                    }
                    else if (col == 1013)
                    {
                        var existingData = context.TLADM_PanelAttributes
                                                       .OrderBy(x => x.Pan_PK).ToList();


                        propertyValues = core.ExtrapNumber(powernumber, existingData.Count());

                        foreach (var row in existingData)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = row.Pan_Description;

                            if (propertyValues.Contains((int)row.Pan_PowerN))
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = true;
                            }
                            else
                            {
                               this.dataGridViewxx.Rows[index].Cells[1].Value = false;
                            }

                            this.dataGridViewxx.Rows[index].Cells[2].Value = row.Pan_PowerN;
                        }
                    }
                    else if (col == 1014)
                    {
                        var existingData = context.TLADM_Labels.Where(x => x.Lbl_Customer_FK == _CustFK)
                                                       .OrderBy(x => x.Lbl_Description).ToList();

                        propertyValues = core.ExtrapNumber(powernumber, existingData.Count());

                        foreach (var row in existingData)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = row.Lbl_Description;

                            if (propertyValues.Contains((int)row.Lbl_PowerN))
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = true;
                            }
                            else
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = false;
                            }

                            this.dataGridViewxx.Rows[index].Cells[2].Value = row.Lbl_PowerN;
                        }
                    }
                    else if (col == 1015)
                    {
                        var existingData = context.TLADM_Labels.OrderBy(x => x.Lbl_Description).ToList();

                        foreach (var row in existingData)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = row.Lbl_Description;

                            if (_TotalPN == (int)row.Lbl_Id)
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = true;
                            }
                            else
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = false;
                            }

                            this.dataGridViewxx.Rows[index].Cells[2].Value = row.Lbl_Id;
                        }
                    }
                    else if (col == 4005)
                    {
                        var existingData = context.TLADM_Yarn
                                                       .OrderBy(x => x.YA_Id).ToList();


                        propertyValues = core.ExtrapNumber(powernumber, existingData.Count());

                        foreach (var row in existingData)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = row.YA_Description;

                            if (propertyValues.Contains((int)row.YA_PowerN))
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = true;
                            }
                            else
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = false;
                            }

                            this.dataGridViewxx.Rows[index].Cells[2].Value = row.YA_PowerN;
                        }
                    }
                    else if (col == 4006)
                    {
                        var existingData = context.TLADM_Yarn
                                                       .OrderBy(x => x.YA_Id).ToList();


                        propertyValues = core.ExtrapNumber(powernumber, existingData.Count());

                        foreach (var row in existingData)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            dataGridViewxx.Rows[index].Cells[0].Value = row.YA_Description;

                            if (propertyValues.Contains((int)row.YA_PowerN))
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = true;
                            }
                            else
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = false;
                            }

                            this.dataGridViewxx.Rows[index].Cells[2].Value = row.YA_PowerN;
                        }
                    }
                    else if (col == 4007)
                    {
                        var existingData = context.TLADM_FabricWeight
                                                       .OrderBy(x => x.FWW_Id).ToList();


                        propertyValues = core.ExtrapNumber(powernumber, existingData.Count());

                        foreach (var row in existingData)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = row.FWW_Description;

                            if (propertyValues.Contains((int)row.FWW_PowerN))
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = true;
                            }
                            else
                            {
                               this.dataGridViewxx.Rows[index].Cells[1].Value = false;
                            }

                            this.dataGridViewxx.Rows[index].Cells[2].Value = row.FWW_PowerN;
                        }

                    }
                    else if (col == 4020)
                    {
                        var CurrentColours = context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).OrderBy(x=>x.Col_Display).ToList();

                        var ExistingData = context.TLADM_GreigeColour.Where(x => x.Grcl_Greige_FK == _TotalPN).ToList();
                        
                        foreach (var row in CurrentColours)
                        {
                            bool flag = false;

                            var GC = ExistingData.Where(x=>x.Grlc_Colour_FK == row.Col_Id).FirstOrDefault();

                            if(GC != null)
                            {
                                flag = true;
                                Parms.Colours.Add(row);
                            }

                            this.comboColours.Items.Add(new Administration.CheckComboBoxItem(row.Col_Id, row.Col_Display, flag));
                        }

                        
                    }
                    else if (col == 5000)
                    {
                        var existingData = context.TLADM_Departments
                                                       .OrderBy(x => x.Dep_Id).ToList();


                        propertyValues = core.ExtrapNumber(powernumber, existingData.Count());

                        foreach (var row in existingData)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = row.Dep_Description;

                            if (propertyValues.Contains((int)row.Dep_PowerN))
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = true;
                            }
                            else
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = false;
                            }

                            this.dataGridViewxx.Rows[index].Cells[2].Value = row.Dep_PowerN;
                        }

                    }

                    else if (col == 6000)
                    {
                        var existingData = context.TLADM_MachineMaintenance
                                                       .OrderBy(x => x.Maint_ShortCode).ToList();


                        propertyValues = core.ExtrapNumber(powernumber, existingData.Count());

                        foreach (var row in existingData)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = row.Maint_Description;

                            if (propertyValues.Contains((int)row.Maint_PowerN))
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = true;
                            }
                            else
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = false;
                            }

                            this.dataGridViewxx.Rows[index].Cells[2].Value = row.Maint_PowerN;
                        }

                    }
                    else if (col == 7000)
                    {
                        var existingData = context.TLADM_Labels
                                                       .OrderBy(x => x.Lbl_Description).ToList();


                        propertyValues = core.ExtrapNumber(powernumber, existingData.Count());

                        foreach (var row in existingData)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = row.Lbl_Description;

                            if (propertyValues.Contains((int)row.Lbl_PowerN))
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = true;
                            }
                            else
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = false;
                            }

                            this.dataGridViewxx.Rows[index].Cells[2].Value = row.Lbl_PowerN;
                        }

                    }

                    else if (col == 17011)
                    {
                        var existingData = context.TLADM_FabricAttributes
                                                       .OrderBy(x => x.FbAtrib_Description).ToList();


                        propertyValues = core.ExtrapNumber(powernumber, existingData.Count());

                        foreach (var row in existingData)
                        {
                            var index = this.dataGridViewxx.Rows.Add();
                            this.dataGridViewxx.Rows[index].Cells[0].Value = row.FbAtrib_Description;

                            if (propertyValues.Contains((int)row.FbAtrib_PowerN))
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = true;
                            }
                            else
                            {
                                this.dataGridViewxx.Rows[index].Cells[1].Value = false;
                            }

                            this.dataGridViewxx.Rows[index].Cells[2].Value = row.FbAtrib_PowerN;
                        }

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //-------------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //--------------------------------------------------------------------------------------
        private void cmboColours_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is Administration.CheckComboBoxItem)
            {
                Administration.CheckComboBoxItem item = (Administration.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    Parms.Colours.Add(repo.LoadColour(item._Pk));

                    var value = Parms.ColoursForDeletion.Find(it => it.Col_Id == item._Pk);
                    if (value != null)
                    {
                        Parms.ColoursForDeletion.Remove(value);
                    }
                }
                else
                {
                    var value = Parms.Colours.Find(it => it.Col_Id == item._Pk);
                    if (value != null)
                    {
                        Parms.Colours.Remove(value);
                        value = Parms.ColoursForDeletion.Find(it => it.Col_Id == item._Pk);
                        if (value == null)
                        {
                            Parms.ColoursForDeletion.Add(value);
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
          
            if (oBtn != null)
            {
                if (_Col != 1006 && _Col != 1007 && _Col != 2006 && _Col != 4020)
                {
                    TotalPN = 0;
                    foreach (DataGridViewRow row in dataGridViewxx.Rows)
                    {
                        if (row.Cells[0].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                            {
                                TotalPN += Convert.ToInt32(row.Cells[2].Value.ToString());
                            }
                        }
                    }

                }
                else if (_Col == 1006)
                {
                    TLADM_StyleColour StyCol = null;
                    using (var context = new TTI2Entities())
                    {
                        foreach (DataGridViewRow row in this.dataGridViewxx.Rows)
                        {
                            if ((bool)row.Cells[1].Value == false)
                            {
                                if (row.Cells[0].Value != null)
                                {
                                    var index = (int)row.Cells[2].Value;

                                    StyCol = context.TLADM_StyleColour.Where(x => x.STYCOL_Style_FK == _TotalPN && x.STYCOL_Colour_FK == index).FirstOrDefault();
                                    if (StyCol != null)
                                    {
                                        if ((bool)row.Cells[1].Value == false)
                                        {
                                            context.TLADM_StyleColour.Remove(StyCol);
                                        }
                                       
                                    }
                                }
                                continue;
                            }
                            else
                            {
                                if (row.Cells[0].Value != null)
                                {
                                    var index = (int)row.Cells[2].Value;

                                    StyCol = context.TLADM_StyleColour.Where(x => x.STYCOL_Style_FK == _TotalPN && x.STYCOL_Colour_FK == index).FirstOrDefault();
                                    if (StyCol == null)
                                    {
                                            StyCol = new TLADM_StyleColour();
                                            StyCol.STYCOL_Colour_FK = index;
                                            StyCol.STYCOL_Style_FK = _TotalPN;

                                            context.TLADM_StyleColour.Add(StyCol);
                                  
                                    }
                                }
                            }
                        }
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                }
                else if (_Col == 1007)
                {
                   TLADM_StyleTrim STrim = null;
                   using (var context = new TTI2Entities())
                    {
                        var SingleRow = (from Rows in dataGridViewxx.Rows.Cast<DataGridViewRow>()
                                         where (bool)Rows.Cells[1].Value == true
                                         select Rows).FirstOrDefault();
                                              
                    }
                }
                else if (_Col == 2006)
                {
                    using (var context = new TTI2Entities())
                    {
                    
                        foreach (DataGridViewRow Row in this.dataGridViewxx.Rows)
                        {
                            TLADM_AuxColours AuxColours = new TLADM_AuxColours();
                            bool Add = false;

                            if(Row.Cells[2].Value == null)
                                continue;

                            if (Row.Cells[0].Value == null)
                                Add = true;

                            if (!Add)
                            {
                                var Pk = (int)Row.Cells[0].Value;
                                AuxColours = context.TLADM_AuxColours.Find(Pk);
                                if (AuxColours != null)
                                {
                                    AuxColours.AuxCol_FinishedCode = Row.Cells[2].Value.ToString();
                                    AuxColours.AuxCol_Description = Row.Cells[3].Value.ToString();
                                }
                            }
                            else
                            {
                                AuxColours.AuxCol_Colour_Fk = _TotalPN;
                                AuxColours.AuxCol_FinishedCode = Row.Cells[2].Value.ToString();
                                AuxColours.AuxCol_Description = Row.Cells[3].Value.ToString();
                            }

                            if (Add)
                                context.TLADM_AuxColours.Add(AuxColours);
                        }

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                }
                else if (_Col == 4020)  // This is the Greige 
                {
                    using (var context = new TTI2Entities())
                    {
                        var ExistingItems = context.TLADM_GreigeColour.Where(x => x.Grcl_Greige_FK == _TotalPN).ToList();
                        foreach (var Item in Parms.ColoursForDeletion )
                        {
                            var DelColour = context.TLADM_GreigeColour.Where(x => x.Grcl_Greige_FK == _TotalPN && x.Grlc_Colour_FK == Item.Col_Id).FirstOrDefault();
                            if (DelColour != null)
                                    context.TLADM_GreigeColour.Remove(DelColour);

                        }
                        foreach (var Clr in Parms.Colours)
                        {
                           var Existing = context.TLADM_GreigeColour.Where(x => x.Grcl_Greige_FK == _TotalPN && x.Grlc_Colour_FK == Clr.Col_Id).FirstOrDefault();
                           if (Existing != null)
                               continue;
                   
                            var GColour = new TLADM_GreigeColour();
                            GColour.Grcl_Greige_FK = _TotalPN;
                            GColour.Grlc_Colour_FK = Clr.Col_Id;

                            context.TLADM_GreigeColour.Add(GColour);
                        }

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                }
            }
            
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                if (_Col == 1006)
                {
                    if (!(bool)oDgv.CurrentCell.EditedFormattedValue)
                    {
                        var CurrentRow = oDgv.CurrentRow;
                        var Index = (int)CurrentRow.Cells[2].Value;

                        using (var context = new TTI2Entities())
                        {
                            var Record = context.TLADM_StyleColour.Where(x => x.STYCOL_Style_FK == _TotalPN && x.STYCOL_Colour_FK == Index).FirstOrDefault();
                            if (Record != null)
                            {
                                context.TLADM_StyleColour.Remove(Record);

                                try
                                {
                                    context.SaveChanges();

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }

                            }

                        }
                    }
                }
                else if (_Col == 1007 && _DoCheck)
                {
                    if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                    {
                        var CurrentRow = oDgv.CurrentRow;
                        using (var context = new TTI2Entities())
                        {
                            var TrimPk = (int)CurrentRow.Cells[2].Value;
                            var StylePk = _TrimKeys[0];

                            foreach (DataGridViewRow Row in dataGridViewxx.Rows)
                            {
                                if (CurrentRow.Index == Row.Index)
                                {
                                    _TrimKeys[1] = (int)CurrentRow.Cells[2].Value; 
                                    continue;
                                }

                                Row.Cells[1].Value = false;
                            }

                            dataGridViewxx.Refresh();
                        }
                            
                    }
                }
                else if (_Col == 1015)
                {
                    var CurrentRow = oDgv.CurrentRow;
                    foreach (DataGridViewRow Row in dataGridViewxx.Rows)
                    {
                        if (CurrentRow.Index == Row.Index)
                            continue;

                        dataGridViewxx.Rows[Row.Index].Cells[1].Value = false;
                    }
                }
            }
        }

        private void comboColours_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true; 
        }
    }
}
