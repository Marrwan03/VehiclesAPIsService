using DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Access
{
    public class clsVehicleDetails
    {
        public static List<VehicleDetailDTO> GetAllVehicleDetails()
        {
            List<VehicleDetailDTO> VehicleDetailsInfo = new List<VehicleDetailDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllVehicleDetails", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VehicleDetailsInfo.Add
                                    (
                                    new VehicleDetailDTO(
                                        reader.GetInt32(reader.GetOrdinal("ID")),
                                        reader.GetInt32(reader.GetOrdinal("MakeID")),
                                        reader.GetInt32(reader.GetOrdinal("ModelID")),
                                        reader.GetInt32(reader.GetOrdinal("SubModelID")),
                                        reader.GetInt32(reader.GetOrdinal("BodyID")),
                                        reader.GetString(reader.GetOrdinal("Vehicle_Display_Name")),
                                        reader.GetInt16(reader.GetOrdinal("Year")),
                                        reader.GetInt32(reader.GetOrdinal("DriveTypeID")),
                                        reader.GetString(reader.GetOrdinal("Engine")),
                                        reader.GetInt16(reader.GetOrdinal("Engine_CC")),
                                        reader.GetByte(reader.GetOrdinal("Engine_Cylinders")),
                                        reader.GetDecimal(reader.GetOrdinal("Engine_Liter_Display")),
                                        reader.GetInt32(reader.GetOrdinal("FuelTypeID")),
                                        reader.GetByte(reader.GetOrdinal("NumDoors"))
                                        ));
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                VehicleDetailsInfo = null;
            }
            return VehicleDetailsInfo;
        }
        public static VehicleDetailDTO GetVehicleDetail(int ID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetVehicleDetailBy", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new VehicleDetailDTO(
                                        reader.GetInt32(reader.GetOrdinal("ID")),
                                        reader.GetInt32(reader.GetOrdinal("MakeID")),
                                        reader.GetInt32(reader.GetOrdinal("ModelID")),
                                        reader.GetInt32(reader.GetOrdinal("SubModelID")),
                                        reader.GetInt32(reader.GetOrdinal("BodyID")),
                                        reader.GetString(reader.GetOrdinal("Vehicle_Display_Name")),
                                        reader.GetInt16(reader.GetOrdinal("Year")),
                                        reader.GetInt32(reader.GetOrdinal("DriveTypeID")),
                                        reader.GetString(reader.GetOrdinal("Engine")),
                                        reader.GetInt16(reader.GetOrdinal("Engine_CC")),
                                        reader.GetByte(reader.GetOrdinal("Engine_Cylinders")),
                                        reader.GetDecimal(reader.GetOrdinal("Engine_Liter_Display")),
                                        reader.GetInt32(reader.GetOrdinal("FuelTypeID")),
                                        reader.GetByte(reader.GetOrdinal("NumDoors"))
                                        );
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public static int? AddNewVehicleDetail(VehicleDetailDTO vDTO)
        {
            int? VehicleDetailID = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_AddNewVehicleDetail", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MakeID", vDTO.MakeID);
                        cmd.Parameters.AddWithValue("@ModelID", vDTO.ModelID);
                        cmd.Parameters.AddWithValue("@SubModelID", vDTO.SubModelID);
                        cmd.Parameters.AddWithValue("@BodyID", vDTO.BodyID);
                        cmd.Parameters.AddWithValue("@Vehicle_Display_Name", vDTO.Vehicle_Display_Name);
                        cmd.Parameters.AddWithValue("@Year", vDTO.Year);
                        cmd.Parameters.AddWithValue("@DriveTypeID", vDTO.DriveTypeID);
                        cmd.Parameters.AddWithValue("@Engine", vDTO.Engine);
                        cmd.Parameters.AddWithValue("@Engine_CC", vDTO.Engine_CC);
                        cmd.Parameters.AddWithValue("@Engine_Cylinders", vDTO.Engine_Cylinders);
                        cmd.Parameters.AddWithValue("@Engine_Liter_Display", vDTO.Engine_Liter_Display);
                        cmd.Parameters.AddWithValue("@FuelTypeID", vDTO.FuelTypeID);
                        cmd.Parameters.AddWithValue("@NumDoors", vDTO.NumDoors);
                        var outputparam = new SqlParameter("@NewID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output,
                        };
                        cmd.Parameters.Add(outputparam);
                        conn.Open();

                        cmd.ExecuteNonQuery();
                        VehicleDetailID = (int)outputparam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return VehicleDetailID;
        }
        public static bool IsVehicleDetailExists(int ID)
        {
            bool Result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_IsVehicleDetailExists", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        var outputparam = new SqlParameter("@Result", SqlDbType.Bit)
                        { Direction = ParameterDirection.Output, };
                        cmd.Parameters.Add(outputparam);
                        conn.Open();

                        cmd.ExecuteNonQuery();
                        Result = (bool)outputparam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Result;
        }
        public static bool DeleteVehicleDetail(int ID)
        {
            int RecordEffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_DeleteVehicleDetail", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        conn.Open();

                        RecordEffected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return RecordEffected > 0;
        }
        public static bool UpdatedVehicleDetail(VehicleDetailDTO vDTO)
        {
            int RecordEffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UpdateVehicleDetailBy", conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", vDTO.VehicleDetailID);
                        cmd.Parameters.AddWithValue("@MakeID", vDTO.MakeID);
                        cmd.Parameters.AddWithValue("@ModelID", vDTO.ModelID);
                        cmd.Parameters.AddWithValue("@SubModelID", vDTO.SubModelID);
                        cmd.Parameters.AddWithValue("@BodyID", vDTO.BodyID);
                        cmd.Parameters.AddWithValue("@Vehicle_Display_Name", vDTO.Vehicle_Display_Name);
                        cmd.Parameters.AddWithValue("@Year", vDTO.Year);
                        cmd.Parameters.AddWithValue("@DriveTypeID", vDTO.DriveTypeID);
                        cmd.Parameters.AddWithValue("@Engine", vDTO.Engine);
                        cmd.Parameters.AddWithValue("@Engine_CC", vDTO.Engine_CC);
                        cmd.Parameters.AddWithValue("@Engine_Cylinders", vDTO.Engine_Cylinders);
                        cmd.Parameters.AddWithValue("@Engine_Liter_Display", vDTO.Engine_Liter_Display);
                        cmd.Parameters.AddWithValue("@FuelTypeID", vDTO.FuelTypeID);
                        cmd.Parameters.AddWithValue("@NumDoors", vDTO.NumDoors);

                        RecordEffected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return RecordEffected > 0;
        }
    }
}
