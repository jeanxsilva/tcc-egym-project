export function formatTime(timeString: string) {
    if(!timeString){
        return;
    }

    let splittedHour = timeString.replace("PT", "").split("H");
    let mountedTime = '';

    splittedHour.forEach((item, index) => {
        if (splittedHour.length > 1 && index == splittedHour.length - 1) {
            if (item.includes("M")) {
                item = item.replace("M", "");

                if (parseInt(item) < 10) {
                    item = "0" + item;
                }
            } else if (item == '') {
                item = "00";
            }

            mountedTime += item;
            return;
        }

        if (parseInt(item) < 10) {
            item = "0" + item;
        }

        mountedTime = item + ":";
    });
    console.log(mountedTime);
    return mountedTime;
}