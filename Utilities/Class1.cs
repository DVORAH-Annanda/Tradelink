using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Data.Entity;
using System.Windows.Forms;
using System.Drawing;

namespace Utilities
{
    public class Util
    {
        bool nonNumeric;

        public int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }


        public void txtWin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumeric)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        public string SuccessFullTransAction()
        {
            return "Record updated successfully to database";
        }

        public void txtWin_KeyDownOEM(object sender, KeyEventArgs e)
        {
            TextBox oTxt = sender as TextBox;

            nonNumeric = false;

            // Determine whether the keystroke is a number from the top of the keyboard. 
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad. 
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace. 
                    if (e.KeyCode != Keys.Back && e.KeyCode != Keys.OemPeriod)
                    {
                        // A non-numerical keystroke was pressed. 
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumeric = true;
                    }
                }

            }

            if (e.KeyCode == Keys.OemPeriod)
            {
                var cnt = oTxt.Text.Count(x => x == '.');
                if (cnt > 0)
                {
                    nonNumeric = true;
                }
            }

            //If shift key was pressed, it's not a number. 
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumeric = true;
            }
        }

        public void txtWin_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumeric = false;

            // Determine whether the keystroke is a number from the top of the keyboard. 
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad. 
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace. 
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed. 
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumeric = true;
                    }
                }

            }
            //If shift key was pressed, it's not a number. 
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumeric = true;
            }
        }

        public bool IsValueDidgit(string TextToCheck)
        {
            bool yesIsDidgit = true;

            foreach (var c in TextToCheck)
            {
                if (!char.IsDigit(c))
                {
                    yesIsDidgit = false;
                    break;
                }
            }
            return yesIsDidgit;
        }

        public bool[] PopulateArray(int ArrLength, bool boolValue)
        {
            var items = Enumerable.Repeat<bool>(boolValue, ArrLength).ToArray();
            return items;
        }

        public string returnMessage(bool[] selectedarray, bool addRec, string[][] flds)
        {
            int Cnt = 0;
            StringBuilder Mess = new StringBuilder();

            foreach (var ArrayElement in selectedarray)
            {
                if (bool.FalseString == ArrayElement.ToString())
                {
                    var result = (from u in flds
                                  where u[2] == Cnt.ToString()
                                  select u).FirstOrDefault();

                    Mess.Append(result[1] + Environment.NewLine);
                }

                Cnt += 1;
            }
            return Mess.ToString();
        }

        public void WriteLog(string message, string FileType)
        {
            string FileExt = ".txt";
            string FileName = FileType + FileExt;
            string Location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = Location + "\\" + FileName;
            string date = DateTime.Now.ToShortDateString();
            if (!File.Exists(path))
            {
                using (StreamWriter outfile =
                new StreamWriter(path))
                {
                    outfile.WriteLine(date + " : " + message, Environment.NewLine);
                    outfile.Close();
                }
            }
            else
            {
                using (StreamWriter outfile = File.AppendText(path))
                {
                    outfile.WriteLine(date + " : " + message, Environment.NewLine);
                    outfile.Close();
                }

            }
        }

        public List<int> ExtrapNumber(int Number, int Cnt)
        {
            List<int> ans = new List<int>();
            while (Number > 0)
            {
                int power = (int)Math.Pow(2.00D, (double)Cnt);
                if (Number >= power)
                {
                    ans.Add(power);
                    Number -= power;
                }
                Cnt -= 1;
            }


            return ans;
        }

        public DateTime CenturyDate(Int32 CDN)
        {
            DateTime centuryBegin = new DateTime(2001, 1, 1);
            DateTime now = centuryBegin.AddDays(CDN);
            return now;


        }

        public Int32 CenturyDayNumber(DateTime today)
        {
            DateTime centuryBegin = new DateTime(2001, 1, 1);
            DateTime currentDate = today;

            long elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);

            return elapsedSpan.Days;

        }

        public DataGridView Get_Styles(DataGridView odgv, int LabelId)
        {
            DataGridView oDgv = odgv;
            oDgv.Columns[0].Width = 180;
            oDgv.Width = oDgv.Columns[0].Width + oDgv.Columns[1].Width + oDgv.Columns[2].Width + oDgv.Columns[3].Width
                         + oDgv.Columns[4].Width + oDgv.Columns[5].Width + oDgv.Columns[6].Width + oDgv.Columns[7].Width + oDgv.Columns[8].Width;

            oDgv.Parent.Width = oDgv.Width;

            oDgv.Rows.Clear();

            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_Styles
                                   .Where(x => x.Sty_Label_FK == LabelId)
                                   .OrderBy(x => x.Sty_Id).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.Sty_Description;

                    if (ExistingRow.Sty_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.Sty_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.Sty_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.Sty_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.Sty_PowerN.ToString();
                    oDgv.Rows[index].Cells[5].Value = ExistingRow.Sty_Size_PowerN.ToString();
                    oDgv.Rows[index].Cells[6].Value = ExistingRow.Sty_Colour_PowerN.ToString();
                    //  oDgv.Rows[index].Cells[7].Value = ExistingRow.Sty_Ribbing_PowerN.ToString();
                    oDgv.Rows[index].Cells[7].Value = ExistingRow.Sty_Trims_PowerN.ToString();
                    oDgv.Rows[index].Cells[8].Value = ExistingRow.Sty_Fabric_PowerN.ToString();
                    oDgv.Rows[index].Cells[9].Value = ExistingRow.Sty_FabricWeight_PowerN.ToString();
                    oDgv.Rows[index].Cells[10].Value = ExistingRow.Sty_FabricGroup_PowerN.ToString();
                }
            }
            return oDgv;
        }

        public bool Save_Style(DataGridView oDgv, int SelectedLabel)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_Styles clrs = null;

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_Styles.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_Styles();
                            lAdd = true;
                        }

                        clrs.Sty_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.Sty_Discontinued = true;
                            else
                                clrs.Sty_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.Sty_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.Sty_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.Sty_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.Sty_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());


                        if (row.Cells[5].Value != null && !String.IsNullOrEmpty(row.Cells[5].Value.ToString()))
                        {
                            clrs.Sty_Size_PowerN = Convert.ToInt32(row.Cells[5].Value.ToString());
                        }
                        else
                            clrs.Sty_Size_PowerN = 0;

                        if (row.Cells[6].Value != null && !String.IsNullOrEmpty(row.Cells[6].Value.ToString()))
                        {
                            clrs.Sty_Colour_PowerN = Convert.ToInt32(row.Cells[6].Value.ToString());
                        }
                        else
                            clrs.Sty_Colour_PowerN = 0;
                        /* 
                         if (row.Cells[7].Value != null && !String.IsNullOrEmpty(row.Cells[7].Value.ToString()))
                         {
                             clrs.Sty_Ribbing_PowerN = Convert.ToInt32(row.Cells[7].Value.ToString());
                         }
                         else
                             clrs.Sty_Ribbing_PowerN = 0;
                         */
                        if (row.Cells[7].Value != null && !String.IsNullOrEmpty(row.Cells[7].Value.ToString()))
                        {
                            clrs.Sty_Trims_PowerN = Convert.ToInt32(row.Cells[7].Value.ToString());
                        }
                        else
                            clrs.Sty_Trims_PowerN = 0;

                        if (row.Cells[8].Value != null && !String.IsNullOrEmpty(row.Cells[8].Value.ToString()))
                        {
                            clrs.Sty_Fabric_PowerN = Convert.ToInt32(row.Cells[8].Value.ToString());
                        }
                        else
                            clrs.Sty_Fabric_PowerN = 0;

                        if (row.Cells[9].Value != null && !String.IsNullOrEmpty(row.Cells[9].Value.ToString()))
                        {
                            clrs.Sty_FabricWeight_PowerN = Convert.ToInt32(row.Cells[9].Value.ToString());
                        }
                        else
                            clrs.Sty_FabricWeight_PowerN = 0;

                        if (row.Cells[10].Value != null && !String.IsNullOrEmpty(row.Cells[9].Value.ToString()))
                        {
                            clrs.Sty_FabricGroup_PowerN = Convert.ToInt32(row.Cells[10].Value.ToString());
                        }
                        else
                            clrs.Sty_FabricGroup_PowerN = 0;

                        clrs.Sty_Label_FK = SelectedLabel;


                        if (lAdd)
                            Context.TLADM_Styles.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }


        public DataGridView Get_Colours(DataGridView odgv)
        {
            DataGridView oDgv = odgv;

            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_Colours
                                   .OrderBy(x => x.Col_Description).ToList();

                ((DataGridViewTextBoxColumn)oDgv.Columns[5]).MaxInputLength = 5;

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.Col_Description;

                    if (ExistingRow.Col_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.Col_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.Col_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.Col_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.Col_PowerN.ToString();
                    oDgv.Rows[index].Cells[5].Value = ExistingRow.Col_FinishedCode;

                    if (ExistingRow.Col_HasAuxColors)
                        oDgv.Rows[index].Cells[6].Value = true;
                    else
                    {
                        oDgv.Rows[index].Cells[6].Value = false;

                    }
                    oDgv.Rows[index].Cells[7].Value = ExistingRow.Col_AuxPowerN.ToString();
                }
            }
            return oDgv;
        }

        public bool Save_Colours(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_Colours clrs = null;

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_Colours.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_Colours();
                            lAdd = true;
                        }

                        clrs.Col_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.Col_Discontinued = true;
                            else
                                clrs.Col_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.Col_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.Col_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.Col_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.Col_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());

                        if (row.Cells[5].Value != null)
                            clrs.Col_FinishedCode = row.Cells[5].Value.ToString();
                        else
                            clrs.Col_FinishedCode = string.Empty;

                        if (row.Cells[6].Value != null)
                        {
                            if (row.Cells[6].Value.ToString() == bool.TrueString)
                                clrs.Col_HasAuxColors = true;
                            else
                                clrs.Col_HasAuxColors = false;

                        }
                        if (row.Cells[7].Value != null)
                        {
                            clrs.Col_AuxPowerN = Convert.ToInt32(row.Cells[7].Value.ToString());
                        }
                        else
                            clrs.Col_AuxPowerN = 0;


                        if (lAdd)
                            Context.TLADM_Colours.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }


        public DataGridView Get_MaintenanceDetail(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            ((DataGridViewTextBoxColumn)oDgv.Columns[3]).MaxInputLength = 5;

            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_MachineMaintenance
                                   .OrderBy(x => x.Maint_ShortCode).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.Maint_ShortCode;
                    oDgv.Rows[index].Cells[1].Value = ExistingRow.Maint_Description;
                    oDgv.Rows[index].Cells[2].Value = ExistingRow.Maint_IntervalDownTime;
                    oDgv.Rows[index].Cells[3].Value = ExistingRow.Maint_Pk;
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.Maint_PowerN;
                    oDgv.Rows[index].Cells[5].Value = ExistingRow.Maint_DownTimePeriod;
                }
            }
            return oDgv;
        }

        public bool Save_MaintenanceDetail(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_MachineMaintenance clrs = null;

                        if (row.Cells[4].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[4].Value.ToString());
                            clrs = Context.TLADM_MachineMaintenance.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_MachineMaintenance();
                            lAdd = true;
                        }

                        clrs.Maint_ShortCode = row.Cells[0].Value.ToString();
                        clrs.Maint_Description = row.Cells[1].Value.ToString();

                        if (row.Cells[2].Value != null)
                        {
                            clrs.Maint_IntervalDownTime = Convert.ToInt32(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.Maint_IntervalDownTime = 0;


                        if (row.Cells[3].Value != null)
                        {
                            clrs.Maint_DownTimePeriod = Convert.ToInt32(row.Cells[3].Value.ToString());
                        }
                        else
                            clrs.Maint_DownTimePeriod = 0;


                        if (row.Cells[5].Value == null && lAdd)
                        {
                            clrs.Maint_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.Maint_PowerN = Convert.ToInt32(row.Cells[5].Value.ToString());





                        if (lAdd)
                            Context.TLADM_MachineMaintenance.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }

        public DataGridView Get_AuxColours(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            ((DataGridViewTextBoxColumn)oDgv.Columns[3]).MaxInputLength = 5;

            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_AuxColours
                                   .OrderBy(x => x.AuxCol_Description).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.AuxCol_Description;
                    oDgv.Rows[index].Cells[1].Value = ExistingRow.AuxCol_Id;
                    oDgv.Rows[index].Cells[2].Value = ExistingRow.AuxCol_PowerN;
                    oDgv.Rows[index].Cells[3].Value = ExistingRow.AuxCol_FinishedCode;

                }
            }
            return oDgv;
        }

        public bool Save_AuxColours(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_AuxColours clrs = null;

                        if (row.Cells[1].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[1].Value.ToString());
                            clrs = Context.TLADM_AuxColours.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_AuxColours();
                            lAdd = true;
                        }

                        clrs.AuxCol_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[2].Value == null && lAdd)
                        {
                            clrs.AuxCol_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.AuxCol_PowerN = Convert.ToInt32(row.Cells[2].Value.ToString());

                        if (row.Cells[3].Value != null)
                            clrs.AuxCol_FinishedCode = row.Cells[3].Value.ToString();
                        else
                            clrs.AuxCol_FinishedCode = string.Empty;

                        if (lAdd)
                            Context.TLADM_AuxColours.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }

        public DataGridView Get_Yarn(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            ((DataGridViewTextBoxColumn)oDgv.Columns[0]).MaxInputLength = 50;


            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_Yarn
                                   .OrderBy(x => x.YA_Id).ToList();
                ;
                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.YA_Description;

                    if (ExistingRow.YA_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.YA_Discontnued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.YA_Discontnued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.YA_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.YA_PowerN.ToString();
                }
            }
            return oDgv;
        }

        public bool Save_Yarn(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_Yarn clrs = null;

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_Yarn.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_Yarn();
                            lAdd = true;
                        }

                        clrs.YA_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.YA_Discontinued = true;
                            else
                                clrs.YA_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.YA_Discontnued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.YA_Discontnued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.YA_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.YA_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());

                        if (lAdd)
                            Context.TLADM_Yarn.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }

        public DataGridView Get_Griege(DataGridView odgv)
        {
            DataGridView oDgv = odgv;

            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_Griege
                                   .OrderBy(x => x.TLGreige_Id).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.TLGreige_Description;

                    if (ExistingRow.TLGreige_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.TLGreige_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.TLGreige_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.TLGreige_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.TLGreige_PowerN.ToString();
                    oDgv.Rows[index].Cells[5].Value = ExistingRow.TLGriege_YarnPowerN.ToString();
                    oDgv.Rows[index].Cells[6].Value = ExistingRow.TLGreige_FWPowerN.ToString();
                    oDgv.Rows[index].Cells[7].Value = ExistingRow.TLGriege_FWTPowerN.ToString();
                }
            }
            return oDgv;
        }

        public bool Save_Griege(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_Griege clrs = null;

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_Griege.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_Griege();
                            lAdd = true;
                        }

                        clrs.TLGreige_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.TLGreige_Discontinued = true;
                            else
                                clrs.TLGreige_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.TLGreige_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.TLGreige_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.TLGreige_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.TLGreige_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());

                        if (row.Cells[5].Value != null && !String.IsNullOrEmpty(row.Cells[5].Value.ToString()))
                        {
                            clrs.TLGriege_YarnPowerN = Convert.ToInt32(row.Cells[5].Value.ToString());
                        }
                        else
                            clrs.TLGriege_YarnPowerN = 0;

                        if (row.Cells[6].Value != null && !String.IsNullOrEmpty(row.Cells[6].Value.ToString()))
                        {
                            clrs.TLGreige_FWPowerN = Convert.ToInt32(row.Cells[6].Value.ToString());
                        }
                        else
                            clrs.TLGreige_FWPowerN = 0;

                        if (row.Cells[7].Value != null && !String.IsNullOrEmpty(row.Cells[7].Value.ToString()))
                        {
                            clrs.TLGriege_FWTPowerN = Convert.ToInt32(row.Cells[7].Value.ToString());
                        }
                        else
                            clrs.TLGriege_FWTPowerN = 0;

                        if (lAdd)
                            Context.TLADM_Griege.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }
        public DataGridView Get_FabricWidth(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            oDgv.Width = oDgv.Columns[0].Width + oDgv.Columns[1].Width + oDgv.Columns[2].Width + 55;

            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_FabWidth
                                   .OrderBy(x => x.FW_Id).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.FW_Description;

                    if (ExistingRow.FW_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.FW_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.FW_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.FW_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.FW_PowerN.ToString();
                }
            }
            return oDgv;
        }

        public bool Save_FabricWidth(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_FabWidth clrs = null;

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_FabWidth.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_FabWidth();
                            lAdd = true;
                        }

                        clrs.FW_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.FW_Discontinued = true;
                            else
                                clrs.FW_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.FW_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.FW_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.FW_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.FW_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());

                        if (lAdd)
                            Context.TLADM_FabWidth.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }
        public DataGridView Get_Ribbing(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            oDgv.Width = oDgv.Columns[0].Width + oDgv.Columns[1].Width + oDgv.Columns[2].Width + 55;
            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_Ribbing
                                   .OrderBy(x => x.RI_Id).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.RI_Description;

                    if (ExistingRow.RI_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.RI_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.RI_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.RI_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.RI_PowerN.ToString();
                }
            }
            return oDgv;
        }

        public bool Save_Ribbing(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_Ribbing clrs = null;

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_Ribbing.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_Ribbing();
                            lAdd = true;
                        }

                        clrs.RI_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.RI_Discontinued = true;
                            else
                                clrs.RI_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.RI_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.RI_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.RI_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.RI_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());

                        if (lAdd)
                            Context.TLADM_Ribbing.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }
        public DataGridView Get_Trims(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            oDgv.Width = oDgv.Columns[0].Width + oDgv.Columns[1].Width + oDgv.Columns[2].Width + oDgv.Columns[5].Width + 55;
            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_Trims
                                   .OrderBy(x => x.TR_Id).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.TR_Description;

                    if (ExistingRow.TR_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.TR_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.TR_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.TR_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.TR_powerN.ToString();

                    if (ExistingRow.TR_Rating == true)
                    {
                        oDgv.Rows[index].Cells[5].Value = true;
                    }
                    else
                        oDgv.Rows[index].Cells[5].Value = false;
                }
            }
            return oDgv;
        }

        public bool Save_Trims(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_Trims clrs = null;

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_Trims.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_Trims();
                            lAdd = true;
                        }

                        clrs.TR_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.TR_Discontinued = true;
                            else
                                clrs.TR_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.TR_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.TR_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.TR_powerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.TR_powerN = Convert.ToInt32(row.Cells[4].Value.ToString());


                        if (row.Cells[5].Value != null)
                        {
                            if (row.Cells[5].Value.ToString() == bool.TrueString)
                                clrs.TR_Rating = true;
                            else
                                clrs.TR_Rating = false;

                        }
                        if (lAdd)
                            Context.TLADM_Trims.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }
            }
            return lTransSuccessful;
        }

        public DataGridView Get_Sizes(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            oDgv.Width = oDgv.Columns[0].Width + oDgv.Columns[1].Width + oDgv.Columns[2].Width + 55;
            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_Sizes
                                   .OrderBy(x => x.SI_id).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.SI_Description;

                    if (ExistingRow.SI_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.SI_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.SI_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.SI_id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.SI_PowerN.ToString();
                }
            }
            return oDgv;
        }

        public bool Save_Sizes(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_Sizes clrs = null;

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_Sizes.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_Sizes();
                            lAdd = true;
                        }

                        clrs.SI_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.SI_Discontinued = true;
                            else
                                clrs.SI_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.SI_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.SI_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.SI_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.SI_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());

                        if (lAdd)
                            Context.TLADM_Sizes.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }

        public DataGridView Get_Labels(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            oDgv.Width = oDgv.Columns[0].Width + oDgv.Columns[1].Width + oDgv.Columns[2].Width + 55;
            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_Labels
                                   .OrderBy(x => x.Lbl_Id).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.Lbl_Description;

                    if (ExistingRow.Lbl_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.Lbl_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.Lbl_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.Lbl_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.Lbl_PowerN.ToString();
                }
            }
            return oDgv;
        }

        public bool Save_Labels(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_Labels clrs = null;

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_Labels.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_Labels();
                            lAdd = true;
                        }

                        clrs.Lbl_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.Lbl_Discontinued = true;
                            else
                                clrs.Lbl_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.Lbl_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.Lbl_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.Lbl_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.Lbl_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());

                        if (lAdd)
                            Context.TLADM_Labels.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }

            return lTransSuccessful;
        }

        public DataGridView Get_FabricProduct(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            oDgv.Width = oDgv.Columns[0].Width + oDgv.Columns[1].Width + oDgv.Columns[2].Width + 55;

            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_FabricProduct
                                   .OrderBy(x => x.FP_Id).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.FP_Description;

                    if (ExistingRow.FP_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.FP_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.FP_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.FP_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.FP_PowerN.ToString();
                }
            }
            return oDgv;
        }

        public bool Save_FabricProduct(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_FabricProduct clrs = null;

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_FabricProduct.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_FabricProduct();
                            lAdd = true;
                        }

                        clrs.FP_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.FP_Discontinued = true;
                            else
                                clrs.FP_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.FP_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.FP_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.FP_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.FP_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());

                        if (lAdd)
                            Context.TLADM_FabricProduct.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }

            return lTransSuccessful;
        }

        public DataGridView Get_FabricWeight(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            oDgv.Width = oDgv.Columns[0].Width + oDgv.Columns[1].Width + oDgv.Columns[2].Width + 55;

            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_FabricWeight
                                   .OrderBy(x => x.FWW_Id).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.FWW_Description;

                    if (ExistingRow.FWW_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.FWW_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.FWW_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.FWW_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.FWW_PowerN.ToString();
                    oDgv.Rows[index].Cells[5].Value = ExistingRow.FWW_Calculation_Value.ToString();
                }
            }
            return oDgv;
        }

        public bool Save_FabricWeight(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_FabricWeight clrs = null;

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_FabricWeight.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_FabricWeight();
                            lAdd = true;
                        }

                        clrs.FWW_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.FWW_Discontinued = true;
                            else
                                clrs.FWW_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.FWW_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.FWW_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.FWW_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.FWW_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());

                        if (row.Cells[5].Value != null)
                        {
                            clrs.FWW_Calculation_Value = Convert.ToInt32(row.Cells[5].Value.ToString());
                        }
                        else
                            clrs.FWW_Calculation_Value = 0;

                        if (lAdd)
                            Context.TLADM_FabricWeight.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }

            return lTransSuccessful;
        }
    }
}
