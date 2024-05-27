import { useEffect, useState } from 'react';
import agent from '../../api/agent';
import LoadinComponent from '../../layout/LoadingComponent';
 // Import the correct component
import dayjs, { Dayjs } from 'dayjs';
import DateCalendarServerRequest from '../../components/DateCalendarWHighlight';

const convertToDayjs = (dates: any) => dates.map((date: { year: number; mounth: number; day: number }) => dayjs(new Date(date.year, date.mounth - 1, date.day)));

export default function Account() {
  const [visits, setVisits] = useState<Dayjs[]>([]);
  const [visitCount, setVisitCount] = useState(0);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    agent.Visists.GetGymVisits().then((response) => {
      setVisitCount(response.count);
      var dates = convertToDayjs(response.dates);
      console.log(dates);
      setVisits(dates);
      setLoading(false);
    })
  }, [])

  if (loading)
    return <LoadinComponent />

  return (
    <DateCalendarServerRequest highlightedDays={visits} />
  );
}
