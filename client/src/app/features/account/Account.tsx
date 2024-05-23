import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider/LocalizationProvider';
import { DateCalendar } from '@mui/x-date-pickers/DateCalendar';
import { useEffect, useState } from 'react';
import agent from '../../api/agent';
import LoadinComponent from '../../layout/LoadingComponent';
import { isAllOf } from '@reduxjs/toolkit';
import { Dayjs } from 'dayjs';

export default function Account() {
  const[visits, setvisits] = useState(new Dayjs);
  const[visitCount, setvisitCount] = useState(0);
  const[loadin, setLoading] = useState(true);

  agent.Visists.GetGymVisits().then((response) => {
      setvisitCount(response.count);
      setvisits(response.dates)
      setLoading(false);
  })

  if(loadin)
    return <LoadinComponent/>

  return (
    <DateCalendarWHighlight highlightedDates={visits}/>
  );
}