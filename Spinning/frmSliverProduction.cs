using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentValidation.Results;
using Spinning.Validators;
using Utilities;

namespace Spinning
{
    public partial class frmSliverProduction : Form
    {
        bool formloaded;
        Util core;
        public frmSliverProduction()
        {
            InitializeComponent();
            core = new Util();

            txtCard1Weight.KeyPress += core.txtWin_KeyPress;
            txtCard1Weight.KeyDown += core.txtWin_KeyDownOEM;
            txtCard2Weight.KeyPress += core.txtWin_KeyPress;
            txtCard2Weight.KeyDown += core.txtWin_KeyDownOEM;
            txtCard3Weight.KeyPress += core.txtWin_KeyPress;
            txtCard3Weight.KeyDown += core.txtWin_KeyDownOEM;
            txtCard4Weight.KeyPress += core.txtWin_KeyPress;
            txtCard4Weight.KeyDown += core.txtWin_KeyDownOEM;

            txtRSB1Weight.KeyPress += core.txtWin_KeyPress;
            txtRSB1Weight.KeyDown += core.txtWin_KeyDownOEM;
            txtRSB2Weight.KeyPress += core.txtWin_KeyPress;
            txtRSB2Weight.KeyDown += core.txtWin_KeyDownOEM;

            SetUp(true);
        }

        void SetUp(bool start)
        {
            formloaded = false;
            if (start)
            {
                using (var context = new TTI2Entities())
                {
                    DateTime selectedDate = dtpYarnProductionDate.Value.Date;
                    DateTime selectedDateEndOfDay = selectedDate.Date.AddHours(24);
                    var Card1Info = context.TLSPN_YarnProductionPerMachine.Where(x => x.YarnProductionDate >= selectedDate && x.YarnProductionDate.Value < selectedDateEndOfDay && x.MachineNo_FK == 92).FirstOrDefault();
                    txtCard1Weight.Text = Card1Info != null ?  Card1Info.YarnProduction.ToString() : string.Empty;

                    var Card2Info = context.TLSPN_YarnProductionPerMachine.Where(x => x.YarnProductionDate >= selectedDate && x.YarnProductionDate.Value < selectedDateEndOfDay && x.MachineNo_FK == 93).FirstOrDefault();
                    txtCard2Weight.Text = Card2Info != null ? Card2Info.YarnProduction.ToString() : string.Empty;

                    var Card3Info = context.TLSPN_YarnProductionPerMachine.Where(x => x.YarnProductionDate >= selectedDate && x.YarnProductionDate.Value < selectedDateEndOfDay && x.MachineNo_FK == 94).FirstOrDefault();
                    txtCard3Weight.Text = Card3Info != null ? Card3Info.YarnProduction.ToString() : string.Empty;

                    var Card4Info = context.TLSPN_YarnProductionPerMachine.Where(x => x.YarnProductionDate >= selectedDate && x.YarnProductionDate.Value < selectedDateEndOfDay && x.MachineNo_FK == 95).FirstOrDefault();
                    txtCard4Weight.Text = Card4Info != null ? Card4Info.YarnProduction.ToString() : string.Empty;

                    var RSB1Info = context.TLSPN_YarnProductionPerMachine.Where(x => x.YarnProductionDate >= selectedDate && x.YarnProductionDate.Value < selectedDateEndOfDay && x.MachineNo_FK == 96).FirstOrDefault();
                    txtRSB1Weight.Text = RSB1Info != null ? RSB1Info.YarnProduction.ToString() : string.Empty;

                    var RSB2Info = context.TLSPN_YarnProductionPerMachine.Where(x => x.YarnProductionDate >= selectedDate && x.YarnProductionDate.Value < selectedDateEndOfDay && x.MachineNo_FK == 97).FirstOrDefault();
                    txtRSB2Weight.Text = RSB2Info != null ? RSB2Info.YarnProduction.ToString() : string.Empty;
                }
            }

            formloaded = true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            
            Button oBtn = sender as Button;
            SpinningMachineProductionTotal spinningMachineTotal = new SpinningMachineProductionTotal();
            
            if (oBtn != null && formloaded)
            {
                if (txtCard1Weight.Text != string.Empty)
                {
                    spinningMachineTotal.Card1Total = decimal.Parse(txtCard1Weight.Text);
                }
                if (txtCard2Weight.Text != string.Empty)
                {
                    spinningMachineTotal.Card2Total = decimal.Parse(txtCard2Weight.Text);
                }
                if (txtCard3Weight.Text != string.Empty)
                {
                    spinningMachineTotal.Card3Total = decimal.Parse(txtCard3Weight.Text);
                }
                if (txtCard4Weight.Text != string.Empty)
                {
                    spinningMachineTotal.Card4Total = decimal.Parse(txtCard4Weight.Text);
                }
                if (txtRSB1Weight.Text != string.Empty)
                {
                    spinningMachineTotal.RSB1Total = decimal.Parse(txtRSB1Weight.Text);
                }
                if (txtRSB2Weight.Text != string.Empty)
                {
                    spinningMachineTotal.RSB2Total = decimal.Parse(txtRSB2Weight.Text);
                }                

                //MachineProductionTotalValidator validator = new MachineProductionTotalValidator();
                //ValidationResult results = validator.Validate(spinningMachineTotal);
                //if (results.IsValid)
                //{
                    SaveYarnProduction(92, spinningMachineTotal.Card1Total);
                    SaveYarnProduction(93, spinningMachineTotal.Card2Total);
                    SaveYarnProduction(94, spinningMachineTotal.Card3Total);
                    SaveYarnProduction(95, spinningMachineTotal.Card4Total);
                    SaveYarnProduction(96, spinningMachineTotal.RSB1Total);
                    SaveYarnProduction(97, spinningMachineTotal.RSB2Total);
                //}
                //else
                //{
                //    StringBuilder errorString = new StringBuilder();
                //    errorString.Append("Please correct the following error(s):");
                //    errorString.AppendLine();

                //    foreach (ValidationFailure failure in results.Errors)
                //    {
                //        errorString.Append($"{failure.ErrorMessage} ").AppendLine();
                //    }
                //    MessageBox.Show(errorString.ToString(),"Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                MessageBox.Show("Records successfully saved to database.");
            }
        }

        private void SaveYarnProduction(int machineNo, decimal yarnProduction)
        {
            DateTime selectedDate = dtpYarnProductionDate.Value;
            if (selectedDate != null)
            {
                using (var context = new TTI2Entities())
                {
                    DateTime selectedDateEndOfDay = selectedDate.Date.AddHours(24);
                    TLSPN_YarnProductionPerMachine yarnProductionInfo = context.TLSPN_YarnProductionPerMachine.Where(x => x.YarnProductionDate >= selectedDate.Date && x.YarnProductionDate < selectedDateEndOfDay && x.MachineNo_FK== machineNo).FirstOrDefault();
                    if (yarnProductionInfo != null)
                    {
                        //yarnProductionInfo.
                        yarnProductionInfo.YarnProduction = yarnProduction;
                        yarnProductionInfo.YarnProductionDate = selectedDate;
                    }
                    else
                    {
                        yarnProductionInfo = new TLSPN_YarnProductionPerMachine();

                        yarnProductionInfo.MachineNo_FK = machineNo;
                        yarnProductionInfo.YarnProductionDate = selectedDate;
                        yarnProductionInfo.YarnProduction = yarnProduction;

                        context.TLSPN_YarnProductionPerMachine.Add(yarnProductionInfo);
                    }
                    try
                    {
                        
                        context.SaveChanges();
                        formloaded = false;
                        SetUp(false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void dtpYarnProductionDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null && formloaded)
            {
                SetUp(true);
            }
        }
    }
}
