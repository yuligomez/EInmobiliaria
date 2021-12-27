package com.example.einmobiliaria.services

import com.example.einmobiliaria.models.`in`.*
import com.example.einmobiliaria.models.out.*
import retrofit2.Call
import retrofit2.Response
import retrofit2.http.*

interface ApiService {

    @GET("/api/apartments")
    fun getAllApartments(): Call<ArrayList<ApartmentBasicInfoModel>>

    @GET("/api/apartments/{id}")
    fun getApartmentById(@Path ("id") id : Int): Call<ApartmentBasicInfoModel>


    @Headers("Content-Type: application/json")
    @POST("/api/apartments")
    fun createAaparment(@Body apartmentModel: ApartmentModel): Call<ApartmentBasicInfoModel>

    @Headers("Content-Type: application/json")
    @PUT("/api/apartments/{id}")
    fun editAaparment(@Path("id") id: Int, @Body apartmentModel: ApartmentModel): Call<ApartmentBasicInfoModel>


    @GET("api/elements/{id}")
    fun getElementsByIdAparment(@Path ("id") id : Int): Call<ArrayList<ElementDetailInfoModel>>

    @Headers("Content-Type: application/json")
    @POST("/api/sessions")
    fun logIn(@Body sessionData: SessionModel): Call<SessionBasicModel>

    @Headers("Content-Type: application/json")
    @POST("/api/users")
    fun register(@Body userData: UserModel): Call<UserBasicInfoModel>


    @Headers("Content-Type: application/json")
    @POST("/api/rentals")
    fun addRental(@Body rentalModel: RentalModel): Call<RentalDetailInfoModel>

    @GET("/api/users")
    fun getUsers(): Call<ArrayList<UserBasicInfoModel>>

    @Headers("Content-Type: application/json")
    @PUT("/api/users/{id}")
    fun editUser(@Path ("id") id : Int, @Body userModel: UserModel): Call<UserBasicInfoModel>

    @GET("/api/checks")
    fun getAllChecks(): Call<ArrayList<CheckDetailInfoModel>>

    @GET("/api/rentals")
    fun getAllRentals(): Call<ArrayList<RentalDetailInfoModel>>

    @Headers("Content-Type: application/json")
    @PUT("api/checks/{id}")
    fun updateCheck(@Path ("id") id : Int, @Body checkModel : CheckPutModel ): Call<CheckDetailInfoModel>

    @Headers("Content-Type: application/json")
    @PUT("api/elements/{id}")
    fun updateElement(@Path ("id") id : Int, @Body elementModel : ElementModel ): Call<ElementDetailInfoModel>

    @Headers("Content-Type: application/json")
    @POST("/api/elements")
    fun addElement(@Body elementModel: ElementModel): Call<ElementDetailInfoModel>

    @Headers("Content-Type: application/json")
    @POST("/api/checks")
    fun postRents(@Body checkModel: CheckModel): Call<PostCheckOut>




}