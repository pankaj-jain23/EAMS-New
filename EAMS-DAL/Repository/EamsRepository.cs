using EAMS_ACore;
using EAMS_ACore.HelperModels;
using EAMS_ACore.IRepository;
using EAMS_ACore.Models;
using EAMS_DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_DAL.Repository
{
    public class EamsRepository : IEamsRepository
    {
        private readonly EamsContext _context;
        public EamsRepository(EamsContext context)
        {
            _context = context;
        }


        #region State Master

        public async Task<List<StateMaster>> GetState()
        {
            var stateList = await _context.StateMaster
                .Include(d => d.DistrictMasters)
                .Select(d => new StateMaster
                {
                    StateCode = d.StateCode,
                    StateName = d.StateName,
                    StateMasterId = d.StateMasterId,
                    StateStatus = d.StateStatus
                })
                .ToListAsync();

            return stateList;
        }
        public async Task<StateMaster> UpdateStateById(StateMaster stateMaster1)
        {
            var stateMaster = _context.StateMaster.Where(d => d.StateMasterId == stateMaster1.StateMasterId).FirstOrDefault();
            stateMaster.StateName = stateMaster1.StateName;


            _context.StateMaster.Update(stateMaster);
            _context.SaveChanges();
            return stateMaster;
        }

        public string AddState(StateMaster stateMaster)
        {
            try
           {

                var stateExist = _context.StateMaster
    .Where(p => p.StateCode == stateMaster.StateCode || p.StateName == stateMaster.StateName).FirstOrDefault();


                if (stateExist == null)
                {
                    _context.StateMaster.Add(stateMaster);
                    _context.SaveChanges();
                    return "State " + stateMaster.StateName + " added successfully!";
                }
                else
                {
                    return "State " + stateExist.StateName + " with the same name already exists.";
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, logging or other actions.
                return "An error occurred while processing the request.";
            }
        }
        #endregion

        #region District Master
        public async Task<List<CombinedMaster>> GetDistrictById(string stateMasterId)
        {


            var stateData = await _context.DistrictMaster
                .Include(d => d.StateMaster)
                .Where(d => d.StateMasterId == Convert.ToInt32(stateMasterId)).Select(d => new CombinedMaster
                {
                    StateId = d.StateMaster.StateMasterId,
                    StateName = d.StateMaster.StateName,
                    DistrictId = d.DistrictMasterId,
                    DistrictName = d.DistrictName,
                    DistrictStatus = d.DistrictStatus,
                    DistrictCode = d.DistrictCode

                })

                .ToListAsync();

            return stateData;
        }
        public async Task<DistrictMaster> UpdateDistrictById(DistrictMaster districtMaster1)
        {
            var districtMaster = _context.DistrictMaster.Where(d => d.DistrictMasterId == districtMaster1.DistrictMasterId).FirstOrDefault();
            districtMaster.DistrictName = districtMaster1.DistrictName;
            _context.DistrictMaster.Update(districtMaster);
            _context.SaveChanges();
            return districtMaster;
        }

        public string AddDistrict(DistrictMaster districtMaster)
        {
            try
            {
                var districtExist = _context.DistrictMaster.Where(p => p.DistrictCode == districtMaster.DistrictCode || p.DistrictName == districtMaster.DistrictName).FirstOrDefault();

                if (districtExist == null)
                {
                    _context.DistrictMaster.Add(districtMaster);
                    _context.SaveChanges();
                    return "District" + districtMaster.DistrictName + "Added Successfully !";
                }
                else
                {
                    return "District" + districtMaster.DistrictName + "Same District Already Exists !";
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, logging or other actions.
                return "An error occurred while processing the request.";
            }
        }

        #endregion

        #region Assembly Master
        public async Task<List<CombinedMaster>> GetAssemblies(string stateId, string districtId)
        {
            var innerJoin = from asemb in _context.AssemblyMaster.Where(d => d.DistrictMasterId == Convert.ToInt32(districtId)) // outer sequence
                            join dist in _context.DistrictMaster // inner sequence 
                            on asemb.DistrictMasterId equals dist.DistrictMasterId // key selector
                            join state in _context.StateMaster // additional join for StateMaster
                            on dist.StateMasterId equals state.StateMasterId // key selector for StateMaster
                            where state.StateMasterId == Convert.ToInt32(stateId) // condition for StateMasterId equal to 21
                            select new CombinedMaster
                            { // result selector 
                                StateName = state.StateName,
                                DistrictId = dist.DistrictMasterId,
                                DistrictName = dist.DistrictName,
                                DistrictCode = dist.DistrictCode,
                                AssemblyId = asemb.AssemblyMasterId,
                                AssemblyName = asemb.AssemblyName,
                                AssemblyCode = asemb.AssemblyCode
                            };

            return await innerJoin.ToListAsync();
        }

        public async Task<AssemblyMaster> UpdateAssembliesById(AssemblyMaster assemblyMaster)
        {
            var assembliesMasterRecord = _context.AssemblyMaster.Where(d => d.AssemblyMasterId == assemblyMaster.AssemblyMasterId).FirstOrDefault();
            //assembliesMaster.AssemblyMasterId=assemblyMaster.AssemblyMasterId;
            assembliesMasterRecord.AssemblyName = assemblyMaster.AssemblyName;
            assembliesMasterRecord.AssemblyCode = assemblyMaster.AssemblyCode;
            assembliesMasterRecord.AssemblyType = assemblyMaster.AssemblyType;
            assembliesMasterRecord.AssemblyStatus = assemblyMaster.AssemblyStatus;

            var ss = _context.AssemblyMaster.Update(assembliesMasterRecord);
            _context.SaveChanges();
            return assembliesMasterRecord;
        }

        public string AddAssemblies(AssemblyMaster assemblyMaster) 
        {
            try 
            {
                var assemblieExist = _context.AssemblyMaster.Where(p => p.AssemblyCode == assemblyMaster.AssemblyCode || p.AssemblyName == assemblyMaster.AssemblyName).FirstOrDefault();

                if (assemblieExist == null)
                {
                    _context.AssemblyMaster.Add(assemblyMaster);
                    _context.SaveChanges();
                    return "Assemblies" + assemblyMaster.AssemblyName + "Added Successfully !";
                }
                else 
                {
                    return "Assemblies" + assemblyMaster.AssemblyName + "Same District Already Exists";
                }
            }

            catch (Exception ex) 
            {
                return "An error occurred while processing the request.";
            }
        }
    

        #endregion

        #region SO Master
        public async Task<List<CombinedMaster>> GetSectorOfficersListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {

            var solist = from so in _context.SectorOfficerMaster.Where(d => d.StateMasterId == Convert.ToInt32(stateMasterId)) // outer sequence
                         join asem in _context.AssemblyMaster
                         on so.SoAssemblyCode equals asem.AssemblyCode
                         join dist in _context.DistrictMaster
                         on asem.DistrictMasterId equals dist.DistrictMasterId
                         where asem.DistrictMasterId == Convert.ToInt32(districtMasterId) && asem.AssemblyMasterId == Convert.ToInt32(assemblyMasterId) // key selector
                         join state in _context.StateMaster
                          on dist.StateMasterId equals state.StateMasterId

                         select new CombinedMaster
                         { // result selector 
                             StateName = state.StateName,
                             DistrictId = dist.DistrictMasterId,
                             DistrictName = dist.DistrictName,
                             DistrictCode = dist.DistrictCode,
                             AssemblyId = asem.AssemblyMasterId,
                             AssemblyName = asem.AssemblyName,
                             AssemblyCode = asem.AssemblyCode,
                             soName = so.SoName,
                             soMobile = so.SoMobile,
                             soMasterId=so.SOMasterId

                         };

            return await solist.ToListAsync();
        }
        public async Task<string> AddSectorOfficer(SectorOfficerMaster addSectorOfficerMaster)
        {
            var soUserExist = _context.SectorOfficerMaster.Where(d => d.SoMobile == addSectorOfficerMaster.SoMobile).FirstOrDefault();
            if (soUserExist == null)
            {
                _context.SectorOfficerMaster.Add(addSectorOfficerMaster);
                _context.SaveChanges();
                return "SO User " + addSectorOfficerMaster.SoName + "added successfully!";
            }
            else
            {
                return "SO User " + addSectorOfficerMaster.SoName + "already exists.";
            }
        }

        public async Task<string> UpdateSectorOfficer(SectorOfficerMaster updatedSectorOfficer)
        {
            var existingSectorOfficer = await _context.SectorOfficerMaster
                                                       .FirstOrDefaultAsync(so => so.SOMasterId == updatedSectorOfficer.SOMasterId);

            if (existingSectorOfficer == null)
            {
                return "SO User not found.";
            }

            // Check if the mobile number is unique among other sector officers (excluding the current one being updated)
            var isMobileUnique = await _context.SectorOfficerMaster
                              .AnyAsync(so => so.SoMobile == updatedSectorOfficer.SoMobile);

            if (isMobileUnique==false)
            {
                existingSectorOfficer.SoName = updatedSectorOfficer.SoName;
                existingSectorOfficer.SoMobile = updatedSectorOfficer.SoMobile;
                existingSectorOfficer.SoOfficeName = updatedSectorOfficer.SoOfficeName;
                existingSectorOfficer.SoAssemblyCode = updatedSectorOfficer.SoAssemblyCode;
                existingSectorOfficer.SoDesignation= updatedSectorOfficer.SoDesignation;
                existingSectorOfficer.SOUpdatedAt = updatedSectorOfficer.SOUpdatedAt;

                _context.SectorOfficerMaster.Update(existingSectorOfficer);
                await _context.SaveChangesAsync();

                return "SO User " + existingSectorOfficer.SoName + " updated successfully!";
            }
            else
            {
                return "SO User with the given mobile number already exists.";
            }
        }

        #endregion

        #region Booth Master
        public async Task<List<CombinedMaster>> GetBoothListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {

            var boothlist = from bt in _context.BoothMaster.Where(d => d.StateMasterId == Convert.ToInt32(stateMasterId) && d.DistrictMasterId == Convert.ToInt32(districtMasterId) && d.AssemblyMasterId == Convert.ToInt32(assemblyMasterId)) // outer sequenc)
                            join asem in _context.AssemblyMaster
                            on bt.AssemblyMasterId equals asem.AssemblyMasterId
                            join dist in _context.DistrictMaster
                            on asem.DistrictMasterId equals dist.DistrictMasterId
                            join state in _context.StateMaster
                             on dist.StateMasterId equals state.StateMasterId

                            select new CombinedMaster
                            {
                                StateName = state.StateName,
                                DistrictId = dist.DistrictMasterId,
                                DistrictName = dist.DistrictName,
                                DistrictCode = dist.DistrictCode,
                                AssemblyId = asem.AssemblyMasterId,
                                AssemblyName = asem.AssemblyName,
                                AssemblyCode = asem.AssemblyCode,
                                BoothMasterId = bt.BoothMasterId,
                                BoothName = bt.BoothName,
                                BoothAuxy = bt.BoothNoAuxy,
                                IsAssigned=bt.IsAssigned


                            };
            var count = boothlist.Count();
            return await boothlist.ToListAsync();
        }

        public async Task<List<CombinedMaster>> GetBoothListByAssemblyId(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {

            var boothlist = from bt in _context.BoothMaster.Where(d => d.StateMasterId == Convert.ToInt32(stateMasterId) && d.DistrictMasterId == Convert.ToInt32(districtMasterId) && d.AssemblyMasterId == Convert.ToInt32(assemblyMasterId)) // outer sequenc)
                            join asem in _context.AssemblyMaster
                            on bt.AssemblyMasterId equals asem.AssemblyMasterId
                            join dist in _context.DistrictMaster
                            on asem.DistrictMasterId equals dist.DistrictMasterId
                            join state in _context.StateMaster
                             on dist.StateMasterId equals state.StateMasterId

                            select new CombinedMaster
                            {
                                StateName = state.StateName,
                                DistrictId = dist.DistrictMasterId,
                                DistrictName = dist.DistrictName,
                                DistrictCode = dist.DistrictCode,
                                AssemblyId = asem.AssemblyMasterId,
                                AssemblyName = asem.AssemblyName,
                                AssemblyCode = asem.AssemblyCode,
                                BoothMasterId = bt.BoothMasterId,
                                BoothName = bt.BoothName,
                                BoothAuxy = bt.BoothNoAuxy
                               

                            };
            var count = boothlist.Count();
            return await boothlist.ToListAsync();
        }

        public string AddBooth(BoothMaster boothMaster)
        {
            try
            {
                // Check for uniqueness
                var boothExist = _context.BoothMaster
                    .FirstOrDefault(p => p.BoothCode_No == boothMaster.BoothCode_No || p.BoothName == boothMaster.BoothName);

                if (boothExist == null)
                {
                    // Set UTC time directly in the model
                    boothMaster.BoothCreatedAt = DateTime.UtcNow;
                    boothMaster.BoothUpdatedAt = DateTime.UtcNow;
                    boothMaster.BoothDeletedAt = DateTime.UtcNow;

                    _context.BoothMaster.Add(boothMaster);
                    _context.SaveChanges();

                    return "Booth " + boothMaster.BoothName + " added successfully!";
                }
                else
                {
                    return "Booth " + boothMaster.BoothName + " with the same name or code already exists.";
                }
            }
            catch (Exception ex)
            {
                // Log the exception details for troubleshooting
                Console.WriteLine(ex.Message);
                return "An error occurred while processing the request.";
            }
        }

        public async Task<string> UpdateBooth(BoothMaster boothMaster)
        {
            if (boothMaster.BoothName != string.Empty)
            {
                var existingbooth = await _context.BoothMaster
                                                           .FirstOrDefaultAsync(so => so.BoothMasterId == boothMaster.BoothMasterId);

                if (existingbooth == null)
                {
                    return "Booth Record not found.";
                }
                else
                {
                    existingbooth.BoothName = boothMaster.BoothName;
                    existingbooth.BoothCode_No = boothMaster.BoothCode_No;
                    existingbooth.BoothNoAuxy = boothMaster.BoothNoAuxy;
                    existingbooth.Longitude = boothMaster.Longitude;
                    existingbooth.Latitude = boothMaster.Latitude;
                    existingbooth.BoothUpdatedAt = boothMaster.BoothUpdatedAt;
                    existingbooth.TotalVoters = boothMaster.TotalVoters;
                   
                    _context.BoothMaster.Update(existingbooth);
                    await _context.SaveChangesAsync();

                    return "SO User " + existingbooth.BoothName.Trim() + " updated successfully!";
                }
            }
            else
            {
                return "Booth updated successfully!";
            }

        }

        public async Task<string> BoothMapping(List<BoothMaster> boothMasters)
        { 
            foreach (var boothMaster in boothMasters)
            { 
                var existingBooth = _context.BoothMaster.Where(d =>
                        d.StateMasterId == boothMaster.StateMasterId &&
                        d.DistrictMasterId == boothMaster.DistrictMasterId &&
                        d.AssemblyMasterId == boothMaster.AssemblyMasterId && d.BoothMasterId==boothMaster.BoothMasterId).FirstOrDefault();

                if (existingBooth != null)
                { 
                        existingBooth.AssignedBy = boothMaster.AssignedBy;
                        existingBooth.AssignedTo = boothMaster.AssignedTo;
                        existingBooth.AssignedOnTime = DateTime.UtcNow;
                        existingBooth.IsAssigned = boothMaster.IsAssigned;
                        _context.BoothMaster.Update(existingBooth);
                        _context.SaveChanges();
                   
                }
                else
                {
                    return "Booth Not Found";
                }
            }

            return "Booths assigned successfully!";
        }

        #endregion

        #region Event Master

        public async Task<List<EventMaster>> GetEventList()
        {
            var eventData = await _context.EventMaster
                .OrderBy(d => d.EventSequence) // Add this line for ordering
                .Select(d => new EventMaster
                {
                    EventMasterId = d.EventMasterId,
                    EventName = d.EventName,
                    EventSequence = d.EventSequence,
                    Status = d.Status
                })
                .ToListAsync();

            return eventData;
        }


        public async Task<EventMaster> UpdateEventById(EventMaster eventMaster1)
        { 
            var eventMaster = _context.EventMaster.Where(d => d.EventMasterId == eventMaster1.EventMasterId).FirstOrDefault();
            eventMaster.EventName = eventMaster1.EventName;
            _context.EventMaster.Update(eventMaster);
            _context.SaveChanges();
            return eventMaster;

        }

        #endregion

        #region PCMaster

        public async Task<List<ParliamentConstituencyMaster>> GetPCList()
        { 
            var pcData = await _context.ParliamentConstituencyMaster.OrderBy(d => d.PCMasterId).Select(d => new ParliamentConstituencyMaster 
            { 
                PCMasterId = d.PCMasterId,
                PcCodeNo  = d.PcCodeNo,
                PcName = d.PcName,
                PcType = d.PcType,
                PcStatus = d.PcStatus
            })
                .ToListAsync();
            return pcData;
        }

        #endregion


        private DateTime? ConvertStringToUtcDateTime(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }

            DateTime dateTime = DateTime.ParseExact(dateString, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
            var dateTime1 = ConvertToUtc(dateTime);

            return dateTime1;
        }




        private DateTime? ConvertToUtc(DateTime? dateTime)
        {

            if (dateTime.HasValue)
            {
                // Specify the time zone for India (Indian Standard Time, IST)
                TimeZoneInfo indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

                // Convert the local time to UTC
                DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(dateTime.Value, indianTimeZone);

                // Ensure the kind of the resulting DateTime is DateTimeKind.Utc
                return DateTime.SpecifyKind(utcTime, DateTimeKind.Utc);
            }

            return null;
        }

    }
}
