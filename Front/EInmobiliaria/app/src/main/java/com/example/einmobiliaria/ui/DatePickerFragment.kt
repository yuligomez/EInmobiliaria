package com.example.einmobiliaria.ui

import android.app.DatePickerDialog
import android.app.Dialog
import android.content.Context
import android.os.Build
import android.os.Bundle
import android.widget.DatePicker
import androidx.annotation.RequiresApi
import androidx.fragment.app.DialogFragment
import java.util.*

class DatePickerFragment(val listener : (day : Int, month : Int , year : Int)-> Unit) : DialogFragment (),
    DatePickerDialog.OnDateSetListener{

    override fun onDateSet(view: DatePicker?, year: Int, month: Int, dayOfMonth: Int) {
        listener(dayOfMonth,month,year)
    }

    @RequiresApi(Build.VERSION_CODES.N)
    override fun onCreateDialog(args: Bundle?): Dialog {
        val calendar = Calendar.getInstance()
        val day = calendar.get(Calendar.DAY_OF_MONTH)
        val month = calendar.get(Calendar.MONTH)
        val year = calendar.get(Calendar.YEAR)
        val picker = DatePickerDialog( activity as Context,  this, year, month, day)
        //picker.datePicker.minDate = calendar.timeInMillis
        return picker
    }

}