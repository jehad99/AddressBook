import React, { useState } from "react";
import ReactDatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

interface DatePickerProps {
  value: Date | null;
  placeholder: string | "Select a date";
  onChange: (date: Date | null) => void;
}

const DatePicker: React.FC<DatePickerProps> = ({ value, onChange,  placeholder}) => {
  return (
    <ReactDatePicker
      selected={value}
      onChange={onChange}
      showYearDropdown
      showMonthDropdown
      dropdownMode="select" // Allows selecting month and year
      dateFormat="yyyy-MM-dd"
      placeholderText={placeholder || "Select a date"}
      className="input input-bordered w-full"
    />
  );
};

export default DatePicker;
