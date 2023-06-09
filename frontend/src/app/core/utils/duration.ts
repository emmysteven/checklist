import {CheckVm} from "@app/core/models";

export function duration(data: CheckVm[]): CheckVm[] {
  data.forEach(data => {
    const startTime = data.startTime.split(":");
    const endTime = data.endTime.split(":");

    const startHour = parseInt(startTime[0], 10);
    const startMinute = parseInt(startTime[1], 10);
    const endHour = parseInt(endTime[0], 10);
    const endMinute = parseInt(endTime[1], 10);

    let totalHours = 0;
    let totalMinutes = 0;

    if (!isNaN(startHour) && !isNaN(startMinute) && !isNaN(endHour) && !isNaN(endMinute)) {
      const startTimeInMinutes = (startHour || 0) * 60 + (startMinute || 0);
      const endTimeInMinutes = (endHour || 0) * 60 + (endMinute || 0);

      const totalTimeInMinutes = endTimeInMinutes - startTimeInMinutes;

      if (totalTimeInMinutes >= 0) {
        totalHours = Math.floor(totalTimeInMinutes / 60);
        totalMinutes = totalTimeInMinutes % 60;
      }
    }

    data.totalTime = `${totalHours ? totalHours + "hr" + (totalHours !== 1 ? "s" : "") +
      " " : ""}${totalMinutes ? totalMinutes + "min" + (totalMinutes !== 1 ? "s" : "") : ""}`;
  });

  return data;
}
