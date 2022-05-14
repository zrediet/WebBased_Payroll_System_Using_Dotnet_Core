using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;
using Payroll.Data;

namespace Payroll.Controllers
{
    public class PayrollSheetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PayrollSheetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PayrollSheets
        public async Task<IActionResult> Index()
        {
            var q = (from x in _context.PayrollSheets
                where x.EmployeeId == null
                select x).ToList();
            if (q.Count > 0)
            {
                _context.PayrollSheets.RemoveRange(q);
                _context.SaveChanges();
            }


            var applicationDbContext = _context.PayrollSheets.Include(p => p.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PayrollSheets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payrollSheet = await _context.PayrollSheets
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payrollSheet == null)
            {
                return NotFound();
            }

            return View(payrollSheet);
        }

        // GET: PayrollSheets/Create
        public IActionResult Create()
        {

            // ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            return View();
        }

        // POST: PayrollSheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,PayStart,PayEnd,BasicSalary,Allowance,OverTime,GrossSalary,IncomeTax,PensionEmployee,PensionCompany,Loan,Penality,OtherDeduction,NetPay,NonTaxableAllowance,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId,PaymentRound")] PayrollSheet payrollSheet)
        {
             
            CalculatePayment(Convert.ToDateTime(payrollSheet.PayStart), Convert.ToDateTime(payrollSheet.PayEnd), payrollSheet.PaymentRound);

            payrollSheet.Id = Guid.NewGuid().ToString();
            payrollSheet.CreationTime = DateTime.Today;

            if (ModelState.IsValid)
            {
                _context.Add(payrollSheet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", payrollSheet.EmployeeId);
            return View(payrollSheet);
        }

        // GET: PayrollSheets/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payrollSheet = await _context.PayrollSheets.FindAsync(id);
            if (payrollSheet == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", payrollSheet.EmployeeId);
            return View(payrollSheet);
        }

        // POST: PayrollSheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmployeeId,PayStart,PayEnd,BasicSalary,Allowance,OverTime,GrossSalary,IncomeTax,PensionEmployee,PensionCompany,Loan,Penality,OtherDeduction,NetPay,NonTaxableAllowance,Id,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,IsDeleted,DeletionTime,DeleterUserId")] PayrollSheet payrollSheet)
        {
            if (id != payrollSheet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payrollSheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayrollSheetExists(payrollSheet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", payrollSheet.EmployeeId);
            return View(payrollSheet);
        }

        // GET: PayrollSheets/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payrollSheet = await _context.PayrollSheets
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payrollSheet == null)
            {
                return NotFound();
            }

            return View(payrollSheet);
        }

        // POST: PayrollSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var payrollSheet = await _context.PayrollSheets.FindAsync(id);
            _context.PayrollSheets.Remove(payrollSheet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayrollSheetExists(string id)
        {
            return _context.PayrollSheets.Any(e => e.Id == id);
        }

        protected void CalculatePayment(DateTime startDate, DateTime endDate, PaymentRound round)
        {
            //db = new AgContext();

            var q = (from x in _context.PayrollSheets
                     where x.PayStart.Equals(startDate) && x.PayEnd.Equals(endDate)
                     select x).ToList();
            if (q.Count > 0)
            {
                _context.PayrollSheets.RemoveRange(q);
                _context.SaveChanges();
            }


            decimal BasicSalary = 0;
            decimal Allowance = 0;
            decimal AllowanceNontax = 0;
            decimal OverTime = 0;
            decimal GrossSalary = 0;
            decimal Tax = 0;
            decimal PenEmployee = 0;
            decimal PenCompany = 0;
            decimal OtherDeduction = 0;
            decimal NetPay = 0;
            decimal ot125 = 0;
            decimal ot150 = 0;
            decimal ot200 = 0;
            decimal ot250 = 0;
            decimal ot125V = 0;
            decimal ot150V = 0;
            decimal ot200V = 0;
            decimal ot250V = 0;
            decimal PresentAllowance = 0;
            //decimal Penality = 0;
            decimal Family = 0;
            //decimal BasicSalaryfield = 0;
            decimal NoDaysPresent = 0;
            decimal EmployeePension = 0;
            decimal CompanyPension = 0;
            decimal TotalDays = 0;

            decimal pfemp = 0;
            decimal pfcomp = 0;

            int WorkingDays = 0;
            int Workinghrs = 8; //Working hour/day



            //Provident Fund 
            //var pfsettingemp = (from f in db.GeneralPayrollSettings
            //                 where f.GeneralPSetting == GeneralPSett.ProvidentFundEmp
            //                 select f).ToList();
            //if (pfsettingemp.Count == 1)
            //{
            //    pfemp = Convert.ToUInt32(pfsettingemp[0].Value);
            //}
            //var pfsettingcomp = (from f in db.GeneralPayrollSettings
            //                    where f.GeneralPSetting == GeneralPSett.ProvidentFundComp
            //                    select f).ToList();
            //if (pfsettingcomp.Count == 1)
            //{
            //    pfcomp = Convert.ToUInt32(pfsettingcomp[0].Value);
            //}

            //Gather Settings
            var PensionSettingEmployee = (from ps in _context.PayrollSettings
                                          where ps.GeneralPSett == GeneralPSett.PensionEmployee && ps.IsDeleted == false
                                          select ps).ToList();

            if (PensionSettingEmployee.Count == 1)
            {
                EmployeePension = Convert.ToDecimal(PensionSettingEmployee[0].Value);

            }
            else
            {
                ViewBag.Message = "No Pension Setting Found";
            }

            var PensionSettingCompany = (from ps in _context.PayrollSettings
                                         where ps.GeneralPSett == GeneralPSett.PensionCompany && ps.IsDeleted == false
                                         select ps).ToList();
            if (PensionSettingCompany.Count == 1)
            {
                //emppension = Convert.ToDecimal(pensettingcomp[0].Value);
                CompanyPension = Convert.ToDecimal(PensionSettingCompany[0].Value);
            }
            else
            {
                ViewBag.Message = "No Pension Setting Found";
            }
            //Total Working Hrs Morning
            //var tothrsmor = (from th in db.GeneralPayrollSettings
            //                 where th.GeneralPSetting == GeneralPSett.MorningAttendance
            //                 select th).ToList();
            //if (tothrsmor.Count == 1)
            //{
            //    totalhrs = totalhrs + Convert.ToDecimal(tothrsmor[0].Value);
            //}

            //Total Working Hrs Afternoon

            //var tothrsaft = (from th in db.GeneralPayrollSettings
            //                 where th.GeneralPSetting == GeneralPSett.AfternoonAttendance
            //                 select th).ToList();
            //if (tothrsaft.Count == 1)
            //{
            //    totalhrs = totalhrs + Convert.ToDecimal(tothrsaft[0].Value);
            //}

            //Working Days

            var WorkingDaysLst = (from wl in _context.PayrollSettings
                                  where wl.GeneralPSett == GeneralPSett.WorkingDays && wl.IsDeleted == false
                                  select wl).ToList();
            if (WorkingDaysLst.Count == 1)
            {
                WorkingDays = Convert.ToInt32(WorkingDaysLst[0].Value);
                //divide the working days by 2 if the Round is Round 1
                //else leave as is
                if (round == PaymentRound.First || round == PaymentRound.Second)
                {
                    WorkingDays = WorkingDays / 2;
                }

            }
            else
            {
                ViewBag.Message = "No Working days found";
            }

            //Working Hours
            //var WorkingHrsLst = (from wl in _context.PayrollSettings
            //                      where wl.GeneralPSett == GeneralPSett.WorkingHours
            //                      select wl).ToList();
            //if (WorkingHrsLst.Count == 1)
            //{
            //    Workinghrs = Convert.ToInt32(WorkingHrsLst[0].Value);
            //}
            //else
            //{
            //    ViewBag.Message = "No Working hours found found";
            //}

            //Normal OT list
            var normalOTlst = (from nOTl in _context.PayrollSettings
                               where nOTl.GeneralPSett == GeneralPSett.NormalOT && nOTl.IsDeleted == false
                               select nOTl).ToList();

            if (normalOTlst.Count == 1)
            {
                ot125V = Convert.ToDecimal(normalOTlst[0].Value);
            }
            else
            {
                ViewBag.Message = "No Ot setting found";
            }

            //Normal OT 2
            var NormalOtList2 = (from nOTl2 in _context.PayrollSettings
                                 where nOTl2.GeneralPSett == GeneralPSett.NormalOT2 && nOTl2.IsDeleted == false
                                 select nOTl2).ToList();
            if (NormalOtList2.Count == 1)
            {
                ot150V = Convert.ToDecimal(NormalOtList2[0].Value);
            }
            else
            {
                ViewBag.Message = "No Ot setting found";
            }

            //Weekend OT List
            var WeekendOlist = (from wkOTl in _context.PayrollSettings
                                where wkOTl.GeneralPSett == GeneralPSett.WeekendOT && wkOTl.IsDeleted == false
                                select wkOTl).ToList();
            if (WeekendOlist.Count == 1)
            {
                ot200V = Convert.ToInt32(WeekendOlist[0].Value);
            }
            else
            {
                ViewBag.Message = "No Ot setting found";
            }
            var holidayotlist = (from hOTl in _context.PayrollSettings
                                 where hOTl.GeneralPSett == GeneralPSett.HolidayOT && hOTl.IsDeleted == false
                                 select hOTl).ToList();
            if (holidayotlist.Count == 1)
            {
                ot250V = Convert.ToInt32(holidayotlist[0].Value);
            }
            else
            {
                ViewBag.Message = "No Ot setting found";
            }

            //Search of Employee that are Active OR Hired recently

            var emp = (from x in _context.Employees
                       where (x.IsDeleted == false || x.HireDate > startDate)
                       select x).ToList();

            if (emp.Count > 0)
            {
                foreach (var item in emp)
                {
                    BasicSalary = 0;
                    Allowance = 0;
                    OverTime = 0;
                    GrossSalary = 0;
                    Tax = 0;
                    PenEmployee = 0;
                    PenCompany = 0;
                    OtherDeduction = 0;
                    NetPay = 0;
                    ot125 = 0;
                    ot150 = 0;
                    ot200 = 0;
                    ot250 = 0;
                    PresentAllowance = 0;
                    //BasicSalaryfield = 0;
                    NoDaysPresent = 0;
                    Family = 0;

                    //IF the Employee is Contract based (Daily laborer)
                    if (item.EmploymentType == EmploymentType.Contract)
                    {
                        //Get Attendance List
                        var attlist = (from x in _context.Attendances
                                       where x.EmployeeId.Equals(item.Id) && x.Date >= startDate && x.Date <= endDate && x.IsDeleted == false
                                       select x).ToList(); 
                        decimal dailysalary = Convert.ToDecimal(item.Salary / WorkingDays);
                        if (attlist.Count > 0)
                        {
                            foreach (var listemp in attlist)
                            {
                                if (listemp.AttendanceType == AttendanceType.Available ||
                                    listemp.AttendanceType == AttendanceType.On_Leave ||
                                    listemp.AttendanceType == AttendanceType.Holiday ||
                                    listemp.AttendanceType == AttendanceType.Day_Off ||
                                    listemp.AttendanceType == AttendanceType.Reason)
                                {
                                    NoDaysPresent += 1;
                                    //NoDaysPresent += TotalDays;
                                } 
                            }
                            //Basic Salary is Number of days on duty * daily Salary
                            if (round == PaymentRound.First || round == PaymentRound.Second)
                            {
                                BasicSalary = (NoDaysPresent * dailysalary)/2;
                            }
                            else
                            {
                                BasicSalary = NoDaysPresent * dailysalary;
                            }
                            
                        }
                    }
                    else if (item.EmploymentType == EmploymentType.Permanent)
                    {
                        var attlist = (from x in _context.Attendances
                            where x.EmployeeId.Equals(item.Id) && x.Date >= startDate && x.Date <= endDate && x.IsDeleted == false
                            select x).ToList(); 

                        decimal dailysalary = Convert.ToDecimal(item.Salary / WorkingDays);
                        if (attlist.Count > 0)
                        {
                            foreach (var listemp in attlist)
                            {
                                if (listemp.AttendanceType == AttendanceType.Available ||
                                    listemp.AttendanceType == AttendanceType.On_Leave ||
                                    listemp.AttendanceType == AttendanceType.Holiday ||
                                    listemp.AttendanceType == AttendanceType.Day_Off ||
                                    listemp.AttendanceType == AttendanceType.Reason)
                                {
                                    NoDaysPresent += 1;
                                    //NoDaysPresent += TotalDays;
                                } 
                            }
                            //Basic Salary is Number of days on duty * daily Salary
                            //Round payment is not applicable for 
                            BasicSalary = NoDaysPresent * dailysalary;
                            //if (round == PaymentRound.First)
                            //{
                            //    BasicSalary = (NoDaysPresent * dailysalary)/2;
                            //}
                            //else
                            //{
                            //    BasicSalary = NoDaysPresent * dailysalary;
                            //}
                            
                        }
                    }
                    else
                    {
                        //If the Employee is terminated with in the date range
                        var terlist = (from x in _context.Terminations
                                       where x.EmployeeId.Equals(item.EmployeeId)
                                       select x).ToList();
                        if (terlist.Count > 0)
                        {
                            foreach (var terd in terlist)
                            {
                                if (terd.TerminationDate > startDate && terd.TerminationDate < endDate)
                                {
                                    decimal totday = Convert.ToDecimal((terd.TerminationDate.Day - startDate.Day));
                                    BasicSalary = (decimal)item.Salary / WorkingDays * totday;
                                }
                            }
                        }
                        else if (item.HireDate > startDate && item.HireDate < endDate)
                        {
                            decimal totday = Convert.ToDecimal((endDate.Day - item.HireDate.Day));
                            BasicSalary = (decimal)item.Salary / WorkingDays * totday;
                        }
                        else
                        {
                            BasicSalary = (decimal)item.Salary;
                        }
                    }


                    // Below code can be for both Contractual and Permanent
                    // Employees Except Pension! 
                    // Pension can only be calculated if the employee is permanent and 
                    // IsPension is True.
                    var qot = _context.Overtimes.Where(P => P.EmployeeId.Equals(item.Id) && P.Date >= startDate && P.Date <= endDate).ToList();
                    foreach (var otlst in qot) //otlst stands for OT List
                    {
                        ot125 += otlst.NormalOT != 0 ? otlst.NormalOT : 0;
                        ot150 += otlst.NormalOT2 != 0 ? otlst.NormalOT2 : 0;
                        ot200 += otlst.WeekendOT != 0 ? otlst.WeekendOT : 0;
                        ot250 += otlst.HolyDayOT != 0 ? otlst.HolyDayOT : 0;
                    }

                    OverTime += (decimal)(((((decimal)item.Salary / WorkingDays) / Workinghrs) * ot125) * ot125V);
                    OverTime += (decimal)(((((decimal)item.Salary / WorkingDays) / Workinghrs) * ot150) * ot150V);
                    OverTime += (decimal)(((((decimal)item.Salary / WorkingDays) / Workinghrs) * ot200) * ot200V);
                    OverTime += (decimal)(((((decimal)item.Salary / WorkingDays) / Workinghrs) * ot250) * ot250V);

                    //var qpen = db.Penalties.Where(P => P.EmployeeId.Equals(item.Id) && P.PenaltyDate > startdate && P.PenaltyDate < enddate).ToList();
                    //var ForFamily = _context.EmployeeFamilies.Where(P => P.EmployeeId.Equals(item.EmployeeId) && P.IsDeleted == false).ToList();

                    //Family = Convert.ToDecimal(ForFamily.Select(P => P.Amount).Sum());

                    var Allowancesal = (from x in _context.EmployeeSalaries
                                        where x.IsDeleted == false && x.EmployeeId == item.Id
                                        select x).ToList();
                    //presentallowance = (decimal)Allowancesal.PresentAllowance - penality;

                    //Allowance = (decimal)Allowancesal.Where(p => p.IsDeleted == false).Select(P => P.TransportAllowance).Sum();
                    
                    //var TransportAllowance = Convert.ToSingle(Allowancesal.Where(p => p.IsDeleted == false).Select(P => P.TransportAllowance).Sum());
                    //var HomeAllowance = Convert.ToSingle(Allowancesal.Where(p => p.IsDeleted == false).Select(P => P.HomeAllowance).Sum());
                    //var OtherAllowance = Convert.ToSingle(Allowancesal.Where(p => p.IsDeleted == false).Select(P => P.OtherAllowance).Sum());

                    if (BasicSalary != 0)
                    {
                        //Allowance = Convert.ToDecimal(TransportAllowance + HomeAllowance + OtherAllowance);
                        Allowance = Convert.ToDecimal(0 + 0 + 0);
                    }
                    

                    //AllowanceNontax = (decimal)Allowancesal.Where(p => p.AllowanceType.AllowanceCategory == AllowanceCategory.Allowance && p.NonTax).Select(P => P.Amount).Sum();
                    //otherdeduction = (decimal)Allowancesal.Where(p => p.AllowanceType.AllowanceCategory == AllowanceCategory.Deduction).Select(P => P.Amount).Sum();

                    GrossSalary = BasicSalary + Allowance + OverTime - Family;


                    if (item.IsPension)
                    {
                        PenCompany = (decimal)(BasicSalary * (CompanyPension / (decimal)100.00));
                        PenEmployee = (decimal)(BasicSalary * (EmployeePension / (decimal)100.00));
                    }
                    //if (item.IsProvident)
                    //{
                    //    pfemp = (decimal)(basicsalary * (pfemp / (decimal)100.00));
                    //    pfcomp = (decimal)(basicsalary * (pfcomp / (decimal)100.00));
                    //}

                    float grosssfortax = Convert.ToSingle(GrossSalary);
                    var qincomtax = _context.IncomeTaxSetting
                        .Where(P => P.ActiveDate <= endDate
                                    && P.StartingAmount <= grosssfortax && (P.EndingAmount >= grosssfortax || P.EndingAmount == 0)
                                    && P.IsDeleted == false).ToList();

                    if (qincomtax.Count > 0)
                    {
                        foreach (var itemtax in qincomtax)
                        {
                            Tax = (GrossSalary * ((decimal)itemtax.Percent / 100)) - (decimal)itemtax.Deductable;
                            break;
                        }
                    }
                    else
                    {
                        ViewBag.Message = "No tax range found";
                    }

                    NetPay = GrossSalary + AllowanceNontax - (Tax + PenEmployee);
                    //Payrollemp payrolls = new Payrollemp();
                    PayrollSheet payrolls = new PayrollSheet();
                    payrolls.Id = Guid.NewGuid().ToString();
                    payrolls.EmployeeId = item.Id;
                    payrolls.PayStart = startDate;
                    payrolls.PayEnd = endDate;
                    payrolls.BasicSalary = Convert.ToSingle(BasicSalary);
                    payrolls.Allowance = Convert.ToSingle(Allowance);
                    payrolls.OverTime = Convert.ToSingle(OverTime);
                    payrolls.GrossSalary = Convert.ToSingle(GrossSalary);
                    payrolls.IncomeTax = Convert.ToSingle(Tax);
                    payrolls.PensionEmployee = Convert.ToSingle(PenEmployee);
                    payrolls.PensionCompany = Convert.ToSingle(PenCompany);
                    payrolls.OtherDeduction = Convert.ToSingle(OtherDeduction);
                    payrolls.NetPay = Convert.ToSingle(NetPay);
                    //payrolls.Pf = (float)pfemp;
                    //payrolls.NonTaxableallowance = (float)AllowanceNontax;
                    
                    payrolls.PaymentRound = round;
                    payrolls.NoDays = WorkingDays;


                    payrolls.CreationTime = DateTime.Now;
                    payrolls.CreatorUserId = "";//User.Identity.GetUserId();

                    if (BasicSalary != 0)
                    {
                        _context.PayrollSheets.Add(payrolls);
                        _context.SaveChanges();
                    }
                    
                }
            }
            else
            {
                ModelState.AddModelError("","No Attendance list found");
            }


        }
    }
}
