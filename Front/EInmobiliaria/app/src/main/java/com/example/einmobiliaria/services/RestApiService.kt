package com.example.einmobiliaria.services

import android.util.Log
import com.example.einmobiliaria.repository.RepositorySingleton
import com.example.einmobiliaria.models.`in`.*
import com.example.einmobiliaria.models.out.*
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response


class RestApiService {



    fun getAllApartments(onResult: (String, ArrayList<ApartmentBasicInfoModel>) -> Unit) {
        val retrofit = ServiceBuilder.buildService(ApiService::class.java)
        retrofit.getAllApartments().enqueue(
                object : Callback<ArrayList<ApartmentBasicInfoModel>> {

                    override fun onFailure(call: Call<ArrayList<ApartmentBasicInfoModel>>, t: Throwable) {
                        onResult(t.message.toString(), ArrayList<ApartmentBasicInfoModel>())
                    }

                    override fun onResponse(call: Call<ArrayList<ApartmentBasicInfoModel>>, response: Response<ArrayList<ApartmentBasicInfoModel>>) {
                        if (response.isSuccessful) {

                            val apartments = response.body()
                            RepositorySingleton.aparments.clear();
                            RepositorySingleton.aparments.addAll(apartments!!)
                            onResult("", apartments!!)

                        } else {
                            onResult(response.errorBody().toString(), ArrayList<ApartmentBasicInfoModel>())
                        }
                    }
                }
        )
    }


    fun getElementsByIdAparment(idAparment: Int, onResult: (String, ArrayList<ElementDetailInfoModel>) -> Unit) {

        val retrofit = ServiceBuilder.buildService(ApiService::class.java)

        retrofit.getElementsByIdAparment(idAparment).enqueue(

                object : Callback<ArrayList<ElementDetailInfoModel>> {

                    override fun onFailure(call: Call<ArrayList<ElementDetailInfoModel>>, t: Throwable) {
                        onResult(t.toString(), ArrayList<ElementDetailInfoModel>())
                    }

                    override fun onResponse(call: Call<ArrayList<ElementDetailInfoModel>>, response: Response<ArrayList<ElementDetailInfoModel>>) {
                        if (response.isSuccessful) {
                            val elements = response.body()
                            if (elements != null) {
                              onResult(response.code().toString(), elements)
                            }

                        } else {
                            onResult(response.errorBody().toString(), ArrayList<ElementDetailInfoModel>())
                        }
                    }
                })
    }

    fun getAllChecks(onResult: (String, ArrayList<CheckDetailInfoModel>) -> Unit) {

        val retrofit = ServiceBuilder.buildService(ApiService::class.java)
        retrofit.getAllChecks().enqueue(

                object : Callback<ArrayList<CheckDetailInfoModel>> {

                    override fun onFailure(call: Call<ArrayList<CheckDetailInfoModel>>, t: Throwable) {
                        onResult(t.message.toString(), ArrayList<CheckDetailInfoModel>())
                    }

                    override fun onResponse(call: Call<ArrayList<CheckDetailInfoModel>>, response: Response<ArrayList<CheckDetailInfoModel>>) {
                        if (response.isSuccessful) {
                            val checks = response.body()
                            if (checks != null) {
                                RepositorySingleton.checks.clear()
                                RepositorySingleton.checks.addAll(checks)
                                Log.d("Exito","Se logró cargar la lista de checks desde el backend ,response code ${response.code()}")
                                onResult(response.code().toString(), checks)

                            }
                        } else {
                            onResult(response.errorBody().toString(), ArrayList<CheckDetailInfoModel>())
                        }
                    }
                })
    }


    fun createAparment(aparmentModel: ApartmentModel, onResult: (ApartmentBasicInfoModel?) -> Unit){
        val retrofit = ServiceBuilder.buildService(ApiService::class.java)
        retrofit.createAaparment(aparmentModel).enqueue(
                object : Callback<ApartmentBasicInfoModel> {
                    override fun onFailure(call: Call<ApartmentBasicInfoModel>, t: Throwable) {
                        onResult(null);
                    }

                    override fun onResponse(call: Call<ApartmentBasicInfoModel>, response: Response<ApartmentBasicInfoModel>) {
                        response.body()?.let { RepositorySingleton.aparmentsRegistered.add(it) }
                        Log.d("Exito", "Se logro crear el apartamento en el backend")
                    }
                }
        )
    }
    fun addElement(elementModel: ElementModel, onResult: (String, ElementDetailInfoModel?) -> Unit) {

        val retrofit = ServiceBuilder.buildService(ApiService::class.java)
        retrofit.addElement(elementModel).enqueue(

                object : Callback<ElementDetailInfoModel> {

                    override fun onFailure(call: Call<ElementDetailInfoModel>, t: Throwable) {
                        onResult(t.toString(), null);
                    }

                    override fun onResponse(call: Call<ElementDetailInfoModel>, response: Response<ElementDetailInfoModel>) {
                        if (response.isSuccessful) {
                            onResult(response.message(), response.body())
                            Log.d("exito",
                                    "respuesta satisfactoria  ${response.code()} , respuesta body ${response.body()}")


                        } else {
                            onResult("no se pudo agregar el elemento",null);
                            Log.d("error",
                                    "error al agregar el elemento , respuesta  ${response.code()} , respuesta body ${response.body()}")

                        }
                    }
                })
    }

    fun getUsersByEmail(email: String, onResult: (UserBasicInfoModel?) -> Unit) {
        val retrofit = ServiceBuilder.buildService(ApiService::class.java)
        retrofit.getUsers().enqueue(
                object : Callback<ArrayList<UserBasicInfoModel>> {
                    override fun onResponse(call: Call<ArrayList<UserBasicInfoModel>>, response: Response<ArrayList<UserBasicInfoModel>>) {
                       if(response.isSuccessful){
                           val users = response.body()!!
                           val user = users.first {
                               it.Email == email
                           }
                           onResult(user)
                       } else {
                           onResult(null)
                       }
                    }
                    override fun onFailure(call: Call<ArrayList<UserBasicInfoModel>>, t: Throwable) {
                        onResult(null)
                    }
                }
        )
    }


    fun addRent(rentalModel: RentalModel, onResult: (String, RentalDetailInfoModel?) -> Unit) {
        val retrofit = ServiceBuilder.buildService(ApiService::class.java)
        retrofit.addRental(rentalModel).enqueue(
                object : Callback<RentalDetailInfoModel> {

                    override fun onFailure(call: Call<RentalDetailInfoModel>, t: Throwable) {
                        onResult(t.toString(), null);
                    }

                    override fun onResponse(call: Call<RentalDetailInfoModel>, response: Response<RentalDetailInfoModel>) {
                        if (response.isSuccessful) {

                            var rental : RentalDetailInfoModel = response.body()!!

                            onResult(response.message(), response.body())
                        } else {
                            onResult("Hay un alquiler dentro de esas fechas",null);
                        }

                    }
                }
        )
    }

    fun logIn(sessionData: SessionModel, onResult: (String?) -> Unit){
        val retrofit = ServiceBuilder.buildService(ApiService::class.java)
        retrofit.logIn(sessionData).enqueue(
                object : Callback<SessionBasicModel> {
                    override fun onResponse(call: Call<SessionBasicModel>, response: Response<SessionBasicModel>) {
                        if (response.isSuccessful) {
                            RepositorySingleton.token = response.body().toString()
                            onResult("")
                        } else {
                            onResult("Email y/o Contraseña inválida")
                        }
                    }

                    override fun onFailure(call: Call<SessionBasicModel>, t: Throwable) {
                        onResult(t.message);
                    }
                }
        )
    }

    fun register(userModel: UserModel, onResult: (UserBasicInfoModel?) -> Unit){
        val retrofit = ServiceBuilder.buildService(ApiService::class.java)
        retrofit.register(userModel).enqueue(
                object : Callback<UserBasicInfoModel> {
                    override fun onResponse(call: Call<UserBasicInfoModel>, response: Response<UserBasicInfoModel>) {
                        if (response.isSuccessful) {
                            onResult(response.body())
                        } else {
                            onResult(null)
                        }
                    }

                    override fun onFailure(call: Call<UserBasicInfoModel>, t: Throwable) {
                        onResult(null)
                    }

                }
        )
    }

    fun editUser(userId: Int, userModel: UserModel, onResult: (String, UserBasicInfoModel?) -> Unit){
        val retrofit = ServiceBuilder.buildService(ApiService::class.java)
        retrofit.editUser(userId,userModel).enqueue(
                object : Callback<UserBasicInfoModel>{
                    override fun onResponse(call: Call<UserBasicInfoModel>, response: Response<UserBasicInfoModel>) {
                        if(response.isSuccessful){
                            RepositorySingleton.user = response.body()
                            onResult("Ok",response.body())
                        } else{
                            onResult("No se pudo actualizar datos",null)
                        }
                    }

                    override fun onFailure(call: Call<UserBasicInfoModel>, t: Throwable) {
                        onResult(t.toString(),null)
                    }

                }
        )
    }


    fun updateCheck(chekId: Int, checkModel : CheckPutModel, onResult: (String, CheckDetailInfoModel?) -> Unit){
        val retrofit = ServiceBuilder.buildService(ApiService::class.java)
        retrofit.updateCheck(chekId,checkModel).enqueue(
                object : Callback<CheckDetailInfoModel>{
                    override fun onResponse(call: Call<CheckDetailInfoModel>, response: Response<CheckDetailInfoModel>) {
                        if(response.isSuccessful){
                            onResult("Ok",response.body())
                        } else{
                            onResult("No se pudo actualizar datos de check",null)
                        }
                    }

                    override fun onFailure(call: Call<CheckDetailInfoModel>, t: Throwable) {
                        onResult(t.toString(),null)
                    }

                }
        )
    }


    fun getRents(onResult: (String, ArrayList<RentalDetailInfoModel>?) -> Unit){
        val retrofit = ServiceBuilder.buildService(ApiService::class.java)
        retrofit.getAllRentals().enqueue(
            object : Callback<ArrayList<RentalDetailInfoModel>>{
                override fun onResponse(
                    call: Call<ArrayList<RentalDetailInfoModel>>,
                    response: Response<ArrayList<RentalDetailInfoModel>>
                ) {
                    if(response.isSuccessful){
                        RepositorySingleton.aparmentsToNotify.clear()
                        RepositorySingleton.aparmentsToNotify.addAll(response.body()!!)
                        onResult(response.message(), response.body())
                    } else{
                        onResult("No hay alquileres vencidos", null)
                    }
                }

                override fun onFailure(call: Call<ArrayList<RentalDetailInfoModel>>, t: Throwable) {
                    onResult(t.toString(),null)
                }
            }
        )
    }

    fun notify(checkModel: CheckModel, onResult: (String, PostCheckOut?) -> Unit){
        val retrofit = ServiceBuilder.buildService(ApiService::class.java)
        retrofit.postRents(checkModel).enqueue(
            object : Callback<PostCheckOut>{
                override fun onResponse(
                    call: Call<PostCheckOut>,
                    response: Response<PostCheckOut>
                ) {
                    if(response.isSuccessful){
                        onResult("Se agregaron los checks correctamente", response.body())
                    } else {
                        onResult("No se pudo notificar correctamente", null)
                    }
                }

                override fun onFailure(call: Call<PostCheckOut>, t: Throwable) {
                    onResult(t.toString(), null)
                }

            }
        )
    }

}
