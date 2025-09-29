import React from "react";
import type { HistoryRecord } from "~/components/history-table/models/history-record";
import MobileTable from "~/components/history-table/mobile-table/mobile-table";
import DesktopTable from "~/components/history-table/desktop-table/desktop-table";

const DUMMY_RECORDS: HistoryRecord[] = [
  {
    id: "1",
    documentNumber: "1234567890",
    documentTypes: [0],
    resultType: 0,
    checkedAt: "2023-10-01T12:00:00Z",
  },
  {
    id: "2",
    documentNumber: "0987654321",
    documentTypes: [],
    resultType: 1,
    checkedAt: "2023-10-02T15:30:00Z",
  },
  {
    id: "3",
    documentNumber: "1122334455",
    documentTypes: [],
    resultType: 5,
    checkedAt: "2023-10-03T09:45:00Z",
  },
];

export default function HistoryDashboard({
  records = DUMMY_RECORDS,
}: {
  records?: HistoryRecord[];
}) {
  return (
    <>
      <DesktopTable records={records} />
      <MobileTable records={records} />
    </>
  );
}
