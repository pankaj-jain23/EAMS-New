﻿using EAMS.Helper;
using EAMS_ACore;
using EAMS_ACore.HelperModels;
using EAMS_ACore.IRepository;
using EAMS_ACore.Models;
using EAMS_DAL.DBContext;
using EAMS_DAL.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EAMS_DAL.Repository
{
    public class EamsRepository : IEamsRepository
    {
        private readonly EamsContext _context;
        public EamsRepository(EamsContext context)
        {
            _context = context;
        }

        #region Common method
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
        #endregion

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
        public async Task<Response> UpdateStateById(StateMaster stateMaster)
        {
            var stateMasterRecord = _context.StateMaster.Where(d => d.StateMasterId == stateMaster.StateMasterId).FirstOrDefault();

            if (stateMasterRecord != null)
            {
                stateMasterRecord.StateName = stateMaster.StateName;
                _context.StateMaster.Update(stateMasterRecord);
                _context.SaveChanges();
                return new Response { Status = RequestStatusEnum.OK, Message = "State Updated Successfully" + stateMaster.StateName };

            }
            else
            {
                return new Response { Status = RequestStatusEnum.NotFound, Message = "State Not Found" + stateMaster.StateName };
            }
        }

        public async Task<Response> AddState(StateMaster stateMaster)
        {
            try
            {

                var stateExist = _context.StateMaster
    .Where(p => p.StateCode == stateMaster.StateCode || p.StateName == stateMaster.StateName).FirstOrDefault();


                if (stateExist == null)
                {
                    _context.StateMaster.Add(stateMaster);
                    _context.SaveChanges();

                    return new Response { Status = RequestStatusEnum.OK, Message = "State Added Successfully" + stateMaster.StateName };
                }
                else
                {

                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "State Name Already Exists" + stateMaster.StateName };
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, logging or other actions.
                return new Response { Status = RequestStatusEnum.BadRequest, Message = ex.Message };
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
        public async Task<Response> UpdateDistrictById(DistrictMaster districtMaster)
        {
            if (districtMaster != null)
            {
                var districtMasterRecord = _context.DistrictMaster.Where(d => d.DistrictMasterId == districtMaster.DistrictMasterId).FirstOrDefault();
                districtMasterRecord.DistrictName = districtMaster.DistrictName;
                _context.DistrictMaster.Update(districtMasterRecord);
                _context.SaveChanges();
                return new Response { Status = RequestStatusEnum.OK, Message = "District Updated Successfully" + districtMaster.DistrictName };
            }
            else
            {
                return new Response { Status = RequestStatusEnum.BadRequest, Message = "District Not Found" + districtMaster.DistrictName };
            }
        }

        public async Task<Response> AddDistrict(DistrictMaster districtMaster)
        {
            try
            {
                var districtExist = _context.DistrictMaster.Where(p => p.DistrictCode == districtMaster.DistrictCode || p.DistrictName == districtMaster.DistrictName).FirstOrDefault();

                if (districtExist == null)
                {
                    _context.DistrictMaster.Add(districtMaster);
                    _context.SaveChanges();
                    return new Response { Status = RequestStatusEnum.OK, Message = "District Added Successfully" + districtMaster.DistrictName };
                }
                else
                {

                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Same District Already Exists" + districtMaster.DistrictName };
                }
            }
            catch (Exception ex)
            {// Handle the exception appropriately, logging or other actions.
                return new Response { Status = RequestStatusEnum.BadRequest, Message = ex.Message };
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

        public async Task<Response> UpdateAssembliesById(AssemblyMaster assemblyMaster)
        {
            var assembliesMasterRecord = _context.AssemblyMaster.Where(d => d.AssemblyMasterId == assemblyMaster.AssemblyMasterId).FirstOrDefault();

            if (assembliesMasterRecord == null)
            {
                assembliesMasterRecord.AssemblyName = assemblyMaster.AssemblyName;
                assembliesMasterRecord.AssemblyCode = assemblyMaster.AssemblyCode;
                assembliesMasterRecord.AssemblyType = assemblyMaster.AssemblyType;
                assembliesMasterRecord.AssemblyStatus = assemblyMaster.AssemblyStatus;

                var ss = _context.AssemblyMaster.Update(assembliesMasterRecord);
                _context.SaveChanges();
                return new Response { Status = RequestStatusEnum.OK, Message = "Assembly Added Successfully" + assemblyMaster.AssemblyName };

            }
            else
            {
                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Assembly Not Found" + assemblyMaster.AssemblyName };
            }
        }

        public async Task<Response> AddAssemblies(AssemblyMaster assemblyMaster)
        {
            try
            {
                var assemblieExist = _context.AssemblyMaster.Where(p => p.AssemblyCode == assemblyMaster.AssemblyCode || p.AssemblyName == assemblyMaster.AssemblyName).FirstOrDefault();

                if (assemblieExist == null)
                {
                    _context.AssemblyMaster.Add(assemblyMaster);
                    _context.SaveChanges();

                    return new Response { Status = RequestStatusEnum.OK, Message = assemblyMaster.AssemblyName + "Added Successfully" };
                }
                else
                {
                    return new Response { Status = RequestStatusEnum.BadRequest, Message = assemblyMaster.AssemblyName + "Same District Already Exists" };

                }
            }

            catch (Exception ex)
            {
                return new Response { Status = RequestStatusEnum.BadRequest, Message = ex.Message };
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
                             soMasterId = so.SOMasterId

                         };

            return await solist.ToListAsync();
        }

        public async Task<SectorOfficerProfile> GetSectorOfficerProfile(string soId)
        {

            var solist = from so in _context.SectorOfficerMaster.Where(d => d.SOMasterId == Convert.ToInt32(soId)) // outer sequence
                         join asem in _context.AssemblyMaster
                         on so.SoAssemblyCode equals asem.AssemblyCode
                         join dist in _context.DistrictMaster
                         on asem.DistrictMasterId equals dist.DistrictMasterId
                         //where asem.DistrictMasterId == Convert.ToInt32(districtMasterId) && asem.AssemblyMasterId == Convert.ToInt32(assemblyMasterId) // key selector
                         join state in _context.StateMaster
                          on dist.StateMasterId equals state.StateMasterId


                         select new SectorOfficerProfile
                         {
                             StateName = state.StateName,
                             DistrictName = dist.DistrictName,
                             AssemblyName = asem.AssemblyName,
                             AssemblyCode = asem.AssemblyCode.ToString(),
                             SoName = so.SoName,
                             BoothNo = _context.BoothMaster.Where(p => p.AssignedTo == soId).Select(p => p.BoothCode_No.ToString()).ToList()


                         };
            var soProfile = solist.FirstOrDefault();


            return soProfile;
        }
        public async Task<Response> AddSectorOfficer(SectorOfficerMaster addSectorOfficerMaster)
        {
            var soUserExist = _context.SectorOfficerMaster.Where(d => d.SoMobile == addSectorOfficerMaster.SoMobile).FirstOrDefault();
            if (soUserExist == null)
            {
                _context.SectorOfficerMaster.Add(addSectorOfficerMaster);
                _context.SaveChanges();
                return new Response { Status = RequestStatusEnum.OK, Message = "SO User" + addSectorOfficerMaster.SoName + " " + "Added Successfully" };


            }
            else
            {
                return new Response { Status = RequestStatusEnum.BadRequest, Message = "SO User" + addSectorOfficerMaster.SoName + " " + "Already Exists" };

            }
        }

        public async Task<Response> UpdateSectorOfficer(SectorOfficerMaster updatedSectorOfficer)
        {
            var existingSectorOfficer = await _context.SectorOfficerMaster
                                                       .FirstOrDefaultAsync(so => so.SOMasterId == updatedSectorOfficer.SOMasterId);

            if (existingSectorOfficer == null)
            {

                return new Response { Status = RequestStatusEnum.BadRequest, Message = "SO User" + updatedSectorOfficer.SoName + " " + "Not found" };
            }

            // Check if the mobile number is unique among other sector officers (excluding the current one being updated)
            var isMobileUnique = await _context.SectorOfficerMaster
                              .AnyAsync(so => so.SoMobile == updatedSectorOfficer.SoMobile);

            if (isMobileUnique == false)
            {
                existingSectorOfficer.SoName = updatedSectorOfficer.SoName;
                existingSectorOfficer.SoMobile = updatedSectorOfficer.SoMobile;
                existingSectorOfficer.SoOfficeName = updatedSectorOfficer.SoOfficeName;
                existingSectorOfficer.SoAssemblyCode = updatedSectorOfficer.SoAssemblyCode;
                existingSectorOfficer.SoDesignation = updatedSectorOfficer.SoDesignation;
                existingSectorOfficer.SOUpdatedAt = updatedSectorOfficer.SOUpdatedAt;

                _context.SectorOfficerMaster.Update(existingSectorOfficer);
                await _context.SaveChangesAsync();


                return new Response { Status = RequestStatusEnum.OK, Message = "SO User" + existingSectorOfficer.SoName + " " + "updated successfully" };
            }
            else
            {
                return new Response { Status = RequestStatusEnum.OK, Message = "SO User WIth given Mobile Number : " + updatedSectorOfficer.SoMobile + " " + "Already Exists" };
            }
        }


        public async Task<List<CombinedMaster>> GetBoothListBySoId(string stateMasterId, string districtMasterId, string assemblyMasterId, string soId)
        {

            var boothlist = from bt in _context.BoothMaster.Where(d => d.StateMasterId == Convert.ToInt32(stateMasterId) && d.DistrictMasterId == Convert.ToInt32(districtMasterId) && d.AssemblyMasterId == Convert.ToInt32(assemblyMasterId) && d.AssignedTo == soId)
                            join asem in _context.AssemblyMaster
                            on bt.AssemblyMasterId equals asem.AssemblyMasterId
                            join dist in _context.DistrictMaster
                            on asem.DistrictMasterId equals dist.DistrictMasterId
                            join state in _context.StateMaster
                             on dist.StateMasterId equals state.StateMasterId

                            select new CombinedMaster
                            {
                                StateId = Convert.ToInt32(stateMasterId),
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
                                IsAssigned = bt.IsAssigned,
                                soMasterId = Convert.ToInt32(soId)


                            };
            var count = boothlist.Count();
            return await boothlist.ToListAsync();
        }
        #endregion

        #region Booth Master
        public async Task<List<CombinedMaster>> GetBoothListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {

            var boothlist = from bt in _context.BoothMaster.Where(d => d.StateMasterId == Convert.ToInt32(stateMasterId) && d.DistrictMasterId == Convert.ToInt32(districtMasterId) && d.AssemblyMasterId == Convert.ToInt32(assemblyMasterId) && d.IsAssigned == false) // outer sequenc)
                            join asem in _context.AssemblyMaster
                            on bt.AssemblyMasterId equals asem.AssemblyMasterId
                            join dist in _context.DistrictMaster
                            on asem.DistrictMasterId equals dist.DistrictMasterId
                            join state in _context.StateMaster
                             on dist.StateMasterId equals state.StateMasterId

                            select new CombinedMaster
                            {
                                StateId = Convert.ToInt32(stateMasterId),
                                DistrictId = dist.DistrictMasterId,
                                AssemblyId = asem.AssemblyMasterId,
                                AssemblyName = asem.AssemblyName,
                                AssemblyCode = asem.AssemblyCode,
                                BoothMasterId = bt.BoothMasterId,
                                BoothName = bt.BoothName,
                                BoothAuxy = bt.BoothNoAuxy,
                                IsAssigned = bt.IsAssigned


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

        public async Task<Response> AddBooth(BoothMaster boothMaster)
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


                    return new Response { Status = RequestStatusEnum.OK, Message = "Booth " + boothMaster.BoothName + " added successfully!" };
                }
                else
                {

                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Booth " + boothMaster.BoothName + " with the same name or code already exists." };
                }
            }
            catch (Exception ex)
            {
                // Log the exception details for troubleshooting


                return new Response { Status = RequestStatusEnum.BadRequest, Message = ex.Message };
            }
        }

        public async Task<Response> UpdateBooth(BoothMaster boothMaster)
        {
            if (boothMaster.BoothName != string.Empty)
            {
                var existingbooth = await _context.BoothMaster.FirstOrDefaultAsync(so => so.BoothMasterId == boothMaster.BoothMasterId);

                if (existingbooth == null)
                {
                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Booth Record Not Found" };
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


                    return new Response { Status = RequestStatusEnum.OK, Message = "Booth" + existingbooth.BoothName.Trim() + " updated successfully!" };
                }
            }
            else
            {
                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Booth Name cannot Be Empty" };

            }

        }

        public async Task<Response> BoothMapping(List<BoothMaster> boothMasters)
        {
            foreach (var boothMaster in boothMasters)
            {
                var existingBooth = _context.BoothMaster.Where(d =>
                        d.StateMasterId == boothMaster.StateMasterId &&
                        d.DistrictMasterId == boothMaster.DistrictMasterId &&
                        d.AssemblyMasterId == boothMaster.AssemblyMasterId && d.BoothMasterId == boothMaster.BoothMasterId).FirstOrDefault();


                if (existingBooth != null)
                {
                    var soExists = _context.SectorOfficerMaster.Any(p => p.SOMasterId == Convert.ToInt32(boothMaster.AssignedTo));
                    if (soExists == true)
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
                        return new Response { Status = RequestStatusEnum.NotFound, Message = "Sector Officer Not Found" };
                    }



                }
                else
                {
                    return new Response { Status = RequestStatusEnum.NotFound, Message = "Booth Not Found" };

                }
            }
            return new Response { Status = RequestStatusEnum.OK, Message = "Booths assigned successfully!" };

        }


        public async Task<Response> ReleaseBooth(BoothMaster boothMaster)
        {
            if (boothMaster.BoothMasterId != null)
            {
                if (boothMaster.IsAssigned == false)
                {
                    var existingbooth = await _context.BoothMaster.FirstOrDefaultAsync(so => so.BoothMasterId == boothMaster.BoothMasterId && so.StateMasterId == boothMaster.StateMasterId && so.DistrictMasterId == so.DistrictMasterId && so.AssemblyMasterId == boothMaster.AssemblyMasterId);

                    if (existingbooth == null)
                    {
                        return new Response { Status = RequestStatusEnum.NotFound, Message = "Booth Record not found." };
                    }
                    else
                    {
                        if (existingbooth.IsAssigned == true)
                        {
                            existingbooth.AssignedBy = string.Empty;
                            existingbooth.AssignedTo = string.Empty;
                            existingbooth.IsAssigned = boothMaster.IsAssigned;
                            _context.BoothMaster.Update(existingbooth);
                            await _context.SaveChangesAsync();

                            return new Response { Status = RequestStatusEnum.OK, Message = "Booth " + existingbooth.BoothName.Trim() + " Unassigned successfully!" };
                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Booth " + existingbooth.BoothName.Trim() + " already Unassigned!" };
                        }
                    }
                }
                else
                {
                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Please unassign first!" };


                }
            }
            else
            {
                return new Response { Status = RequestStatusEnum.NotFound, Message = "Record not found!" };

            }
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


        public async Task<Response> UpdateEventById(EventMaster eventMaster1)
        {
            if (eventMaster1.EventName != null && eventMaster1.EventSequence != null)
            {

                var eventMaster = _context.EventMaster.Where(d => d.EventMasterId == eventMaster1.EventMasterId).FirstOrDefault();
                if (eventMaster != null)
                {
                    eventMaster.EventName = eventMaster1.EventName;
                    _context.EventMaster.Update(eventMaster);
                    _context.SaveChanges();
                    return new Response { Status = RequestStatusEnum.OK, Message = "Event+" + eventMaster1.EventName + " " + "added successfully" };
                }
                else
                {
                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Record not Found" };
                }

            }

            else
            {
                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Event Name & Sequence cannot Be Empty" };

            }

        }

        //public async Task<List<EventWiseBooth>> GetBoothListByEventId(string eventId, string soId)
        //{
        //    var soTotalBooths = _context.BoothMaster.Where(p => p.AssignedTo == soId).ToList();
        //    List<EventWiseBooth> list = new List<EventWiseBooth>();

        //    foreach (var boothRecord in soTotalBooths)
        //    {
        //        var electioInfoRecord = _context.ElectionInfoMaster.FirstOrDefault(d =>
        //                   d.BoothMasterId == boothRecord.BoothMasterId);
        //        if (electioInfoRecord is not null)
        //        {
        //            if (eventId == "1")
        //            {
        //                EventWiseBooth eventWiseBooth = new EventWiseBooth()
        //                {
        //                    StateMasterId = boothRecord.StateMasterId,
        //                    DistrictMasterId = boothRecord.DistrictMasterId,
        //                    AssemblyMasterId = boothRecord.AssemblyMasterId,
        //                    BoothMasterId = boothRecord.BoothMasterId,
        //                    BoothName = boothRecord.BoothName,
        //                    BoothCode = boothRecord.BoothCode_No,
        //                    EventMasterId = electioInfoRecord.EventMasterId,
        //                    EventName = _context.EventMaster
        //                    .Where(d => d.EventMasterId == Convert.ToInt32(eventId))
        //                    .Select(e => e.EventName)
        //                    .FirstOrDefault(),
        //                    UpdateStatus = electioInfoRecord.IsPartyDispatched ?? false

        //                };
        //                list.Add(eventWiseBooth);
        //            }
        //            else if (eventId == "2")
        //            {
        //                EventWiseBooth eventWiseBooth = new EventWiseBooth()
        //                {
        //                    StateMasterId = boothRecord.StateMasterId,
        //                    DistrictMasterId = boothRecord.DistrictMasterId,
        //                    AssemblyMasterId = boothRecord.AssemblyMasterId,
        //                    BoothMasterId = boothRecord.BoothMasterId,
        //                    BoothName = boothRecord.BoothName,
        //                    BoothCode = boothRecord.BoothCode_No,
        //                    EventMasterId = electioInfoRecord.EventMasterId,
        //                    EventName = _context.EventMaster
        //                    .Where(d => d.EventMasterId == Convert.ToInt32(eventId))
        //                    .Select(e => e.EventName)
        //                    .FirstOrDefault(),
        //                    UpdateStatus = electioInfoRecord.IsPartyReached ?? false

        //                };
        //                list.Add(eventWiseBooth);
        //            }
        //            else if (eventId == "3")
        //            {
        //                EventWiseBooth eventWiseBooth = new EventWiseBooth()
        //                {
        //                    StateMasterId = boothRecord.StateMasterId,
        //                    DistrictMasterId = boothRecord.DistrictMasterId,
        //                    AssemblyMasterId = boothRecord.AssemblyMasterId,
        //                    BoothMasterId = boothRecord.BoothMasterId,
        //                    BoothName = boothRecord.BoothName,
        //                    BoothCode = boothRecord.BoothCode_No,
        //                    EventMasterId = electioInfoRecord.EventMasterId,
        //                    EventName = _context.EventMaster
        //                    .Where(d => d.EventMasterId == Convert.ToInt32(eventId))
        //                    .Select(e => e.EventName)
        //                    .FirstOrDefault(),
        //                    UpdateStatus = electioInfoRecord.IsSetupOfPolling ?? false

        //                };
        //                list.Add(eventWiseBooth);
        //            }
        //            else if (eventId == "4")
        //            {
        //                EventWiseBooth eventWiseBooth = new EventWiseBooth()
        //                {
        //                    StateMasterId = boothRecord.StateMasterId,
        //                    DistrictMasterId = boothRecord.DistrictMasterId,
        //                    AssemblyMasterId = boothRecord.AssemblyMasterId,
        //                    BoothMasterId = boothRecord.BoothMasterId,
        //                    BoothName = boothRecord.BoothName,
        //                    BoothCode = boothRecord.BoothCode_No,
        //                    EventMasterId = electioInfoRecord.EventMasterId,
        //                    EventName = _context.EventMaster
        //                    .Where(d => d.EventMasterId == Convert.ToInt32(eventId))
        //                    .Select(e => e.EventName)
        //                    .FirstOrDefault(),
        //                    UpdateStatus = electioInfoRecord.IsMockPollDone ?? false

        //                };
        //                list.Add(eventWiseBooth);
        //            }
        //            else if (eventId == "5")
        //            {
        //                EventWiseBooth eventWiseBooth = new EventWiseBooth()
        //                {
        //                    StateMasterId = boothRecord.StateMasterId,
        //                    DistrictMasterId = boothRecord.DistrictMasterId,
        //                    AssemblyMasterId = boothRecord.AssemblyMasterId,
        //                    BoothMasterId = boothRecord.BoothMasterId,
        //                    BoothName = boothRecord.BoothName,
        //                    BoothCode = boothRecord.BoothCode_No,
        //                    EventMasterId = electioInfoRecord.EventMasterId,
        //                    EventName = _context.EventMaster
        //                    .Where(d => d.EventMasterId == Convert.ToInt32(eventId))
        //                    .Select(e => e.EventName)
        //                    .FirstOrDefault(),
        //                    UpdateStatus = electioInfoRecord.IsPollStarted ?? false

        //                };
        //                list.Add(eventWiseBooth);
        //            }

        //            else if (eventId == "7")
        //            {
        //                bool isQueue = electioInfoRecord.VoterInQueue != null;
        //                EventWiseBooth eventWiseBooth = new EventWiseBooth()
        //                {
        //                    StateMasterId = boothRecord.StateMasterId,
        //                    DistrictMasterId = boothRecord.DistrictMasterId,
        //                    AssemblyMasterId = boothRecord.AssemblyMasterId,
        //                    BoothMasterId = boothRecord.BoothMasterId,
        //                    BoothName = boothRecord.BoothName,
        //                    BoothCode = boothRecord.BoothCode_No,
        //                    EventMasterId = electioInfoRecord.EventMasterId,
        //                    EventName = _context.EventMaster
        //                    .Where(d => d.EventMasterId == Convert.ToInt32(eventId))
        //                    .Select(e => e.EventName)
        //                    .FirstOrDefault(),
        //                    UpdateStatus = isQueue

        //                };
        //                list.Add(eventWiseBooth);
        //            }
        //            else if (eventId == "8")
        //            {
        //                bool isFinalVotes = electioInfoRecord.FinalTVote != null;
        //                EventWiseBooth eventWiseBooth = new EventWiseBooth()
        //                {
        //                    StateMasterId = boothRecord.StateMasterId,
        //                    DistrictMasterId = boothRecord.DistrictMasterId,
        //                    AssemblyMasterId = boothRecord.AssemblyMasterId,
        //                    BoothMasterId = boothRecord.BoothMasterId,
        //                    BoothName = boothRecord.BoothName,
        //                    BoothCode = boothRecord.BoothCode_No,
        //                    EventMasterId = electioInfoRecord.EventMasterId,
        //                    EventName = _context.EventMaster
        //                    .Where(d => d.EventMasterId == Convert.ToInt32(eventId))
        //                    .Select(e => e.EventName)
        //                    .FirstOrDefault(),
        //                    UpdateStatus = isFinalVotes

        //                };
        //                list.Add(eventWiseBooth);
        //            }
        //            else if (eventId == "9")
        //            {
        //                EventWiseBooth eventWiseBooth = new EventWiseBooth()
        //                {
        //                    StateMasterId = boothRecord.StateMasterId,
        //                    DistrictMasterId = boothRecord.DistrictMasterId,
        //                    AssemblyMasterId = boothRecord.AssemblyMasterId,
        //                    BoothMasterId = boothRecord.BoothMasterId,
        //                    BoothName = boothRecord.BoothName,
        //                    BoothCode = boothRecord.BoothCode_No,
        //                    EventMasterId = electioInfoRecord.EventMasterId,
        //                    EventName = _context.EventMaster
        //                    .Where(d => d.EventMasterId == Convert.ToInt32(eventId))
        //                    .Select(e => e.EventName)
        //                    .FirstOrDefault(),
        //                    UpdateStatus = electioInfoRecord.IsPollEnded ?? false

        //                };
        //                list.Add(eventWiseBooth);
        //            }
        //            else if (eventId == "10")
        //            {
        //                EventWiseBooth eventWiseBooth = new EventWiseBooth()
        //                {
        //                    StateMasterId = boothRecord.StateMasterId,
        //                    DistrictMasterId = boothRecord.DistrictMasterId,
        //                    AssemblyMasterId = boothRecord.AssemblyMasterId,
        //                    BoothMasterId = boothRecord.BoothMasterId,
        //                    BoothName = boothRecord.BoothName,
        //                    BoothCode = boothRecord.BoothCode_No,
        //                    EventMasterId = electioInfoRecord.EventMasterId,
        //                    EventName = _context.EventMaster
        //                    .Where(d => d.EventMasterId == Convert.ToInt32(eventId))
        //                    .Select(e => e.EventName)
        //                    .FirstOrDefault(),
        //                    UpdateStatus = electioInfoRecord.IsMCESwitchOff ?? false

        //                };
        //                list.Add(eventWiseBooth);
        //            }
        //            else if (eventId == "11")
        //            {
        //                EventWiseBooth eventWiseBooth = new EventWiseBooth()
        //                {
        //                    StateMasterId = boothRecord.StateMasterId,
        //                    DistrictMasterId = boothRecord.DistrictMasterId,
        //                    AssemblyMasterId = boothRecord.AssemblyMasterId,
        //                    BoothMasterId = boothRecord.BoothMasterId,
        //                    BoothName = boothRecord.BoothName,
        //                    BoothCode = boothRecord.BoothCode_No,
        //                    EventMasterId = electioInfoRecord.EventMasterId,
        //                    EventName = _context.EventMaster
        //                    .Where(d => d.EventMasterId == Convert.ToInt32(eventId))
        //                    .Select(e => e.EventName)
        //                    .FirstOrDefault(),
        //                    UpdateStatus = electioInfoRecord.IsPartyDeparted ?? false

        //                };
        //                list.Add(eventWiseBooth);
        //            }
        //            else if (eventId == "12")
        //            {
        //                EventWiseBooth eventWiseBooth = new EventWiseBooth()
        //                {
        //                    StateMasterId = boothRecord.StateMasterId,
        //                    DistrictMasterId = boothRecord.DistrictMasterId,
        //                    AssemblyMasterId = boothRecord.AssemblyMasterId,
        //                    BoothMasterId = boothRecord.BoothMasterId,
        //                    BoothName = boothRecord.BoothName,
        //                    BoothCode = boothRecord.BoothCode_No,
        //                    EventMasterId = electioInfoRecord.EventMasterId,
        //                    EventName = _context.EventMaster
        //                    .Where(d => d.EventMasterId == Convert.ToInt32(eventId))
        //                    .Select(e => e.EventName)
        //                    .FirstOrDefault(),
        //                    UpdateStatus = electioInfoRecord.IsPartyReachedCollectionCenter ?? false

        //                };
        //                list.Add(eventWiseBooth);
        //            }
        //            else if (eventId == "13")
        //            {
        //                EventWiseBooth eventWiseBooth = new EventWiseBooth()
        //                {
        //                    StateMasterId = boothRecord.StateMasterId,
        //                    DistrictMasterId = boothRecord.DistrictMasterId,
        //                    AssemblyMasterId = boothRecord.AssemblyMasterId,
        //                    BoothMasterId = boothRecord.BoothMasterId,
        //                    BoothName = boothRecord.BoothName,
        //                    BoothCode = boothRecord.BoothCode_No,
        //                    EventMasterId = electioInfoRecord.EventMasterId,
        //                    EventName = _context.EventMaster
        //                    .Where(d => d.EventMasterId == Convert.ToInt32(eventId))
        //                    .Select(e => e.EventName)
        //                    .FirstOrDefault(),
        //                    UpdateStatus = electioInfoRecord.IsEVMDeposited ?? false

        //                };
        //                list.Add(eventWiseBooth);
        //            }
        //        }
        //        else
        //        {

        //            EventWiseBooth eventWiseBooth = new EventWiseBooth()
        //            {
        //                StateMasterId = boothRecord.StateMasterId,
        //                DistrictMasterId = boothRecord.DistrictMasterId,
        //                AssemblyMasterId = boothRecord.AssemblyMasterId,
        //                BoothMasterId = boothRecord.BoothMasterId,
        //                BoothName = boothRecord.BoothName,
        //                BoothCode = boothRecord.BoothCode_No,
        //                EventMasterId = Convert.ToInt32(eventId),
        //                EventName = _context.EventMaster
        //                    .Where(d => d.EventMasterId == Convert.ToInt32(eventId))
        //                    .Select(e => e.EventName)
        //                    .FirstOrDefault(),
        //                UpdateStatus = false

        //            };
        //            list.Add(eventWiseBooth);

        //        }

        //    }



        //    return list;
        //}

        public async Task<List<EventWiseBooth>> GetBoothListByEventId(string eventId, string soId)
        {
            var soTotalBooths = _context.BoothMaster.Where(p => p.AssignedTo == soId).ToList();
            List<EventWiseBooth> list = new List<EventWiseBooth>();

            foreach (var boothRecord in soTotalBooths)
            {
                var electioInfoRecord = _context.ElectionInfoMaster.FirstOrDefault(d =>
                    d.BoothMasterId == boothRecord.BoothMasterId);

                if (electioInfoRecord is not null)
                {
                    EventWiseBooth eventWiseBooth = new EventWiseBooth()
                    {
                        StateMasterId = boothRecord.StateMasterId,
                        DistrictMasterId = boothRecord.DistrictMasterId,
                        AssemblyMasterId = boothRecord.AssemblyMasterId,
                        BoothMasterId = boothRecord.BoothMasterId,
                        BoothName = boothRecord.BoothName,
                        BoothCode = boothRecord.BoothCode_No,
                        EventMasterId = electioInfoRecord.EventMasterId,
                        EventName = _context.EventMaster
                            .Where(d => d.EventMasterId == Convert.ToInt32(eventId))
                            .Select(e => e.EventName)
                            .FirstOrDefault(),
                        UpdateStatus = GetUpdateStatus(eventId, electioInfoRecord)
                    };

                    list.Add(eventWiseBooth);
                }
                else
                {
                    EventWiseBooth eventWiseBooth = new EventWiseBooth()
                    {
                        StateMasterId = boothRecord.StateMasterId,
                        DistrictMasterId = boothRecord.DistrictMasterId,
                        AssemblyMasterId = boothRecord.AssemblyMasterId,
                        BoothMasterId = boothRecord.BoothMasterId,
                        BoothName = boothRecord.BoothName,
                        BoothCode = boothRecord.BoothCode_No,
                        EventMasterId = Convert.ToInt32(eventId),
                        EventName = _context.EventMaster
                            .Where(d => d.EventMasterId == Convert.ToInt32(eventId))
                            .Select(e => e.EventName)
                            .FirstOrDefault(),
                        UpdateStatus = false
                    };

                    list.Add(eventWiseBooth);
                }
            }

            return list;
        }

        private bool GetUpdateStatus(string eventId, ElectionInfoMaster electioInfoRecord)
        {
            switch (eventId)
            {
                case "1":
                    return electioInfoRecord.IsPartyDispatched ?? false;
                case "2":
                    return electioInfoRecord.IsPartyReached ?? false;
                case "3":
                    return electioInfoRecord.IsSetupOfPolling ?? false;
                case "4":
                    return electioInfoRecord.IsMockPollDone ?? false;
                case "5":
                    return electioInfoRecord.IsPollStarted ?? false;
                case "7":
                    return electioInfoRecord.VoterInQueue != null;
                case "8":
                    return electioInfoRecord.FinalTVote != null;
                case "9":
                    return electioInfoRecord.IsPollEnded ?? false;
                case "10":
                    return electioInfoRecord.IsMCESwitchOff ?? false;
                case "11":
                    return electioInfoRecord.IsPartyDeparted ?? false;
                case "12":
                    return electioInfoRecord.IsPartyReachedCollectionCenter ?? false;
                case "13":
                    return electioInfoRecord.IsEVMDeposited ?? false;
                default:
                    return false;
            }
        }



        #endregion

        #region PCMaster

        public async Task<List<ParliamentConstituencyMaster>> GetPCList()
        {
            var pcData = await _context.ParliamentConstituencyMaster.OrderBy(d => d.PCMasterId).Select(d => new ParliamentConstituencyMaster
            {
                PCMasterId = d.PCMasterId,
                PcCodeNo = d.PcCodeNo,
                PcName = d.PcName,
                PcType = d.PcType,
                PcStatus = d.PcStatus
            })
                .ToListAsync();
            return pcData;
        }

        #endregion

        #region EventActivity

        public async Task<Response> EventActivity(ElectionInfoMaster electionInfoMaster)
        {
            try
            {
                var electionRecord = await _context.ElectionInfoMaster.Where(d => d.StateMasterId == electionInfoMaster.StateMasterId &&
                         d.DistrictMasterId == electionInfoMaster.DistrictMasterId && d.AssemblyMasterId == electionInfoMaster.AssemblyMasterId &&
                         d.BoothMasterId == electionInfoMaster.BoothMasterId).FirstOrDefaultAsync();

                var boothExists = await _context.BoothMaster.AnyAsync(p => p.BoothMasterId == electionInfoMaster.BoothMasterId && p.StateMasterId == electionInfoMaster.StateMasterId && p.DistrictMasterId == electionInfoMaster.DistrictMasterId && p.BoothMasterId == electionInfoMaster.BoothMasterId && p.IsAssigned == true);

                if (electionRecord != null)
                {
                    
                    _context.ElectionInfoMaster.Update(electionInfoMaster);
                    _context.SaveChanges();
                    return new Response { Status = RequestStatusEnum.OK, Message = "Status Updated Successfully" };
                }
                else
                {
                    if (boothExists == true)
                    {
                        
                        if (electionInfoMaster.EventMasterId == 1)
                        {

                            _context.ElectionInfoMaster.Add(electionInfoMaster);
                            _context.SaveChanges();
                            return new Response { Status = RequestStatusEnum.OK, Message = "Status Added Successfully" };
                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Party Not Dispatched yet" };
                        }

                    }
                    else
                    {
                        return new Response { Status = RequestStatusEnum.NotFound, Message = "Record Not Found, Also Recheck Booth Assigned or not" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = RequestStatusEnum.BadRequest, Message = ex.Message };
            }


        }
        public async Task<ElectionInfoMaster> EventUpdationStatus(ElectionInfoMaster electionInfoMaster)
        {
            var electionInfoRecord = _context.ElectionInfoMaster.Where(d => d.StateMasterId == electionInfoMaster.StateMasterId
            && d.DistrictMasterId == electionInfoMaster.DistrictMasterId &&
            d.AssemblyMasterId == electionInfoMaster.AssemblyMasterId
            && d.BoothMasterId == electionInfoMaster.BoothMasterId
            ).FirstOrDefault();
            return electionInfoRecord;
        }

        public async Task<List<EventWiseBoothStatus>> EventWiseBoothStatus(string soId)
        {
            var soTotalBooths = _context.BoothMaster.Where(p => p.AssignedTo == soId).ToList();

            List<EventWiseBoothStatus> list = new List<EventWiseBoothStatus>();

            int totalPartyDispatched = 0; int totalPartyReached = 0; int totalIsetUpPolling = 0;
            int totalmockpoll = 0; int totalpollstarted = 0; int totalvoterinqueue = 0; int totalfinalvotes = 0;
            int totalpollended = 0; int totalmcevm = 0; int totalpartdeparted = 0; int totalpartycollectoncentre = 0;
            int totalevmdeposited = 0;

            int pendingPartyDispatched = 0;
            int pendingPartyReached = 0;
            int pendingIsetUpPolling = 0;
            int pendingMockPoll = 0;
            int pendingPollStarted = 0;
            int pendingVoterInQueue = 0;
            int pendingFinalVotes = 0;
            int pendingPollEnded = 0;
            int pendingMCEVM = 0;
            int pendingPartDeparted = 0;
            int pendingPartyCollectOnCentre = 0;
            int pendingEVMDeposited = 0;

            //except voterturn out, queue and finl votes

            foreach (var boothRecord in soTotalBooths)
            {
                var electioInfoRecord = _context.ElectionInfoMaster.FirstOrDefault(d =>
                            d.BoothMasterId == boothRecord.BoothMasterId);
                if (electioInfoRecord != null)
                {
                    if (electioInfoRecord.IsPartyDispatched == true)
                    {
                        totalPartyDispatched += 1;
                    }

                    if (electioInfoRecord.IsPartyReached == true)
                    {
                        totalPartyReached += 1;
                    }
                    if (electioInfoRecord.IsSetupOfPolling == true)
                    {
                        totalIsetUpPolling += 1;
                    }
                    if (electioInfoRecord.IsMockPollDone == true)
                    {
                        totalmockpoll += 1;
                    }
                    if (electioInfoRecord.IsPollStarted == true)
                    {
                        totalpollstarted += 1;
                    }
                    if (electioInfoRecord.IsPollEnded == true)
                    {
                        totalpollended += 1;
                    }
                    if (electioInfoRecord.IsMCESwitchOff == true)
                    {
                        totalmcevm += 1;
                    }
                    if (electioInfoRecord.IsPartyDeparted == true)
                    {
                        totalpartdeparted += 1;
                    }
                    if (electioInfoRecord.IsPartyReachedCollectionCenter == true)
                    {
                        totalpartycollectoncentre += 1;
                    }
                    if (electioInfoRecord.IsEVMDeposited == true)
                    {
                        totalevmdeposited += 1;
                    }
                }
            }


            pendingPartyDispatched = soTotalBooths.Count - totalPartyDispatched;
            pendingPartyReached = soTotalBooths.Count - totalPartyReached;
            pendingIsetUpPolling = soTotalBooths.Count - totalIsetUpPolling;
            pendingMockPoll = soTotalBooths.Count - totalmockpoll;
            pendingPollStarted = soTotalBooths.Count - totalpollstarted;
            pendingPollEnded = soTotalBooths.Count - totalpollended;
            pendingMCEVM = soTotalBooths.Count - totalmcevm;
            pendingPartDeparted = soTotalBooths.Count - totalpartdeparted;
            pendingPartyCollectOnCentre = soTotalBooths.Count - totalpartycollectoncentre;
            pendingEVMDeposited = soTotalBooths.Count - totalevmdeposited;
            var event_lits = _context.EventMaster.Where(p => p.Status == true).OrderBy(p => p.EventSequence).ToList();
            foreach (var eventid in event_lits)
            {
                if (eventid.EventMasterId == 1)
                {
                    EventWiseBoothStatus model = new EventWiseBoothStatus();
                    model.EventMasterId = eventid.EventMasterId;
                    model.EventName = eventid.EventName;
                    model.Completed = totalPartyDispatched;
                    model.Pending = pendingPartyDispatched;
                    model.TotalBooths = soTotalBooths.Count;
                    list.Add(model);
                }
                else if (eventid.EventMasterId == 2)
                {
                    EventWiseBoothStatus model = new EventWiseBoothStatus();
                    model.EventMasterId = eventid.EventMasterId;
                    model.EventName = eventid.EventName;
                    model.Completed = totalPartyReached;
                    model.Pending = pendingPartyReached;
                    model.TotalBooths = soTotalBooths.Count;
                    list.Add(model);
                }
                else if (eventid.EventMasterId == 3)
                {
                    EventWiseBoothStatus model = new EventWiseBoothStatus();
                    model.EventMasterId = eventid.EventMasterId;
                    model.EventName = eventid.EventName;
                    model.Completed = totalIsetUpPolling;
                    model.Pending = pendingIsetUpPolling;
                    model.TotalBooths = soTotalBooths.Count;
                    list.Add(model);
                }
                else if (eventid.EventMasterId == 4)
                {
                    EventWiseBoothStatus model = new EventWiseBoothStatus();
                    model.EventMasterId = eventid.EventMasterId;
                    model.EventName = eventid.EventName;
                    model.Completed = totalmockpoll;
                    model.Pending = pendingMockPoll;
                    model.TotalBooths = soTotalBooths.Count;
                    list.Add(model);
                }
                else if (eventid.EventMasterId == 5)
                {
                    EventWiseBoothStatus model = new EventWiseBoothStatus();
                    model.EventMasterId = eventid.EventMasterId;
                    model.EventName = eventid.EventName;
                    model.Completed = totalpollstarted;
                    model.Pending = pendingPollStarted;
                    model.TotalBooths = soTotalBooths.Count;
                    list.Add(model);
                }
                else if (eventid.EventMasterId == 9)
                {
                    EventWiseBoothStatus model = new EventWiseBoothStatus();
                    model.EventMasterId = eventid.EventMasterId;
                    model.EventName = eventid.EventName;
                    model.Completed = totalpollended;
                    model.Pending = pendingPollEnded;
                    model.TotalBooths = soTotalBooths.Count;
                    list.Add(model);
                }
                else if (eventid.EventMasterId == 10)
                {
                    EventWiseBoothStatus model = new EventWiseBoothStatus();
                    model.EventMasterId = eventid.EventMasterId;
                    model.EventName = eventid.EventName;
                    model.Completed = totalmcevm;
                    model.Pending = pendingMCEVM;
                    model.TotalBooths = soTotalBooths.Count;
                    list.Add(model);
                }
                else if (eventid.EventMasterId == 11)
                {
                    EventWiseBoothStatus model = new EventWiseBoothStatus();
                    model.EventMasterId = eventid.EventMasterId;
                    model.EventName = eventid.EventName;
                    model.Completed = totalpartdeparted;
                    model.Pending = pendingPartDeparted;
                    model.TotalBooths = soTotalBooths.Count;
                    list.Add(model);
                }
                else if (eventid.EventMasterId == 12)
                {
                    EventWiseBoothStatus model = new EventWiseBoothStatus();
                    model.EventMasterId = eventid.EventMasterId;
                    model.EventName = eventid.EventName;
                    model.Completed = totalpartycollectoncentre;
                    model.Pending = pendingPartyCollectOnCentre;
                    model.TotalBooths = soTotalBooths.Count;
                    list.Add(model);
                }
                else if (eventid.EventMasterId == 13)
                {
                    EventWiseBoothStatus model = new EventWiseBoothStatus();
                    model.EventMasterId = eventid.EventMasterId;
                    model.EventName = eventid.EventName;
                    model.Completed = totalevmdeposited;
                    model.Pending = pendingEVMDeposited;
                    model.TotalBooths = soTotalBooths.Count;
                    list.Add(model);
                }

            }


            return list;
        }

        #endregion

        #region SendDashBoardCount 
        public async Task<DashBoardRealTimeCount> GetDashBoardCount()
        {
            var electionInfoList = await _context.ElectionInfoMaster.ToListAsync();

            var dashboardCount = new DashBoardRealTimeCount();
            dashboardCount.Total = electionInfoList.Count;
            dashboardCount.Events = new List<EventCount>();
            AddEventCount(dashboardCount, "PartyDispatch", e => e.IsPartyDispatched == true);
            AddEventCount(dashboardCount, "PartyArrived", e => e.IsPartyReached == true);
            AddEventCount(dashboardCount, "SetupPollingStation", e => e.IsSetupOfPolling == true);
            AddEventCount(dashboardCount, "MockPollDone", e => e.IsMockPollDone == true);
            AddEventCount(dashboardCount, "PollStarted", e => e.IsPollStarted == true);
            AddEventCount(dashboardCount, "PollEnded", e => e.IsPollEnded == true);
            AddEventCount(dashboardCount, "MCEVMOff", e => e.IsMCESwitchOff == true);
            AddEventCount(dashboardCount, "PartyDeparted", e => e.IsPartyDeparted == true);
            AddEventCount(dashboardCount, "PartyReachedAtCollection", e => e.IsPartyReachedCollectionCenter == true);
            AddEventCount(dashboardCount, "EVMDeposited", e => e.IsEVMDeposited == true);

            return dashboardCount;
        }

        private void AddEventCount(DashBoardRealTimeCount dashboardCount, string eventName, Func<ElectionInfoMaster, bool> condition)
        {
            var count = _context.ElectionInfoMaster.Count(condition);
            dashboardCount.Events.Add(new EventCount { EventName = eventName, Count = count });
        }

        #endregion

        #region SlotManagement
        public async Task<Response> AddEventSlot(List<SlotManagementMaster> slotManagement)
        {
            
            _context.SlotManagement.AddRange(slotManagement);
            _context.SaveChanges();

            return new Response()
            {Status=RequestStatusEnum.OK
               
            };
        }

        public async Task<List<SlotManagementMaster>> GetEventSlotList()
        {
             return await _context.SlotManagement.ToListAsync();
        }
        #endregion


    }
}
