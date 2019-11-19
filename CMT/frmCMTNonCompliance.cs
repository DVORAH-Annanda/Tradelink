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


namespace CMT
{
    public partial class frmCMTNonCompliance : Form
    {
        bool FormLoaded;
        public DataGridViewTextBoxColumn oTxtBoxA;
        public DataGridViewCheckBoxColumn oChkA;
        public DataGridViewTextBoxColumn oTxtBoxB;
        public DataGridViewTextBoxColumn oTxtBoxC;
        Util core;

        IList<TLCMT_LineIssue> LineIssue = null;

        int _Pk = 0;

        public frmCMTNonCompliance(int Pk)
        {
            InitializeComponent();

            _Pk = Pk;

            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.ReadOnly = true;
            oTxtBoxA.Visible = false;
            oTxtBoxA.HeaderText = "Primary Key";

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "select";

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.ReadOnly = true;
            oTxtBoxB.Visible = false;
            oTxtBoxB.HeaderText = "Primary Key CurrentEntries";

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.ReadOnly = true;
            oTxtBoxC.HeaderText = "Description";
            oTxtBoxC.Width = 155;


            dataGridView1.Columns.Add(oTxtBoxA);
            dataGridView1.Columns.Add(oTxtBoxB);
            
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns.Add(oTxtBoxC);
            
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;

            core = new Util();
        }

        private void frmCMTNonCompliance_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                if(_Pk == 0)
                    LineIssue = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_IssuedToLine && !x.TLCMTLI_WorkCompleted && x.TLCMTLI_CutSheetDetails.Length > 0).OrderBy(x => x.TLCMTLI_CutSheetDetails).ToList();
                else
                    LineIssue = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_Pk == _Pk).OrderBy(x => x.TLCMTLI_CutSheetDetails).ToList();

                cmboCutSheets.DataSource = LineIssue;
                cmboCutSheets.ValueMember = "TLCMTLI_Pk";
                cmboCutSheets.DisplayMember = "TLCMTLI_CutSheetDetails";

                cmboCutSheets.SelectedValue = -1;
                              
                var Entries = context.TLADM_CMTNonCompliance.OrderBy(x => x.CMTNC_ShortCode).ToList();
                foreach (var Entry in Entries)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = Entry.CMTNC_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = null;
                    dataGridView1.Rows[index].Cells[2].Value = false;
                    dataGridView1.Rows[index].Cells[3].Value = Entry.CMTNC_Description;

                }
            }

            txtStyle.Text = string.Empty;
            txtSizes.Text = string.Empty;
            txtColours.Text = string.Empty;

            FormLoaded = true;
        }

        private void cmboCutSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && FormLoaded)
            {
                var TickedRows= (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                 where (bool)Rows.Cells[2].Value == true
                                 select Rows).ToList();
                
                foreach(var TickedRow in TickedRows)
                {
                    TickedRow.Cells[1].Value = null;
                    TickedRow.Cells[2].Value = false;
                }

                var LineIssue = (TLCMT_LineIssue)oCmbo.SelectedItem;
                if (LineIssue != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var CutSheet = context.TLCUT_CutSheet.Find(LineIssue.TLCMTLI_CutSheet_FK);
                        if (CutSheet != null)
                        {
                            txtStyle.Text = context.TLADM_Styles.Find(CutSheet.TLCutSH_Styles_FK).Sty_Description;
                            txtColours.Text = context.TLADM_Colours.Find(CutSheet.TLCutSH_Colour_FK).Col_Display;
                        }

                        var CurrentEntries = context.TLCMT_NonCompliance.Where(x => x.CMTNCD_CutSheet_Fk == LineIssue.TLCMTLI_CutSheet_FK).ToList();
                        foreach (var CurrentEntry in CurrentEntries)
                        {
                            var SingleRow = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                             where (int)Rows.Cells[0].Value == CurrentEntry.CMTNCD_NonCompliance_Fk
                                             select Rows).FirstOrDefault();

                            if (SingleRow != null)
                            {
                                SingleRow.Cells[1].Value = CurrentEntry.CMTNCD_Pk;
                                SingleRow.Cells[2].Value = true;
                            }
                        }
                    }
               }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            TLCMT_NonCompliance NonC = null ; 

            if (oBtn != null && FormLoaded)
            {
                var Selected = (TLCMT_LineIssue)cmboCutSheets.SelectedItem;
                if(Selected == null)
                {
                    MessageBox.Show("Please select a CutSheet from the list available");
                    return;
                }

                DataGridView oDgv = dataGridView1;

                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow Row in oDgv.Rows)
                    {
                        if ((bool)Row.Cells[2].Value == true)
                        {
                            if (Row.Cells[1].Value == null)
                            {
                                NonC = new TLCMT_NonCompliance();
                                NonC.CMTNCD_TransDate = dtpTransDate.Value;
                                NonC.CMTNCD_CutSheet_Fk = Selected.TLCMTLI_CutSheet_FK;
                                NonC.CMTNCD_Applicable = (bool)Row.Cells[2].Value;
                                NonC.CMTNCD_NonCompliance_Fk = (int)Row.Cells[0].Value;
                                NonC.CMTNCD_Year = dtpTransDate.Value.Year;
                                NonC.CMTNCD_WeekNumber = core.GetIso8601WeekOfYear(dtpTransDate.Value);
                                NonC.CMTNCD_Line_FK = (int)Selected.TLCMTLI_LineNo_FK;
                                
                                var CutSheet = context.TLCUT_CutSheet.Find(Selected.TLCMTLI_CutSheet_FK);
                                if (CutSheet != null)
                                    NonC.CMTNCD_Style_FK = CutSheet.TLCutSH_Styles_FK;

                                context.TLCMT_NonCompliance.Add(NonC);
                            }
                        }
                        else
                        {
                            if (Row.Cells[1].Value != null)
                            {
                                var Pk = (int)Row.Cells[1].Value;

                                var CurrentEntry = context.TLCMT_NonCompliance.Find(Pk);
                                if (CurrentEntry != null)
                                {
                                    context.TLCMT_NonCompliance.Remove(CurrentEntry);
                                }
                            }
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to database");
                        
                        frmCMTViewRep vRep = new frmCMTViewRep(27, NonC.CMTNCD_CutSheet_Fk);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(); 

                        cmboCutSheets.DataSource = null;
                        cmboCutSheets.Items.Clear();
                        dataGridView1.Rows.Clear();

                        frmCMTNonCompliance_Load(this, null);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
