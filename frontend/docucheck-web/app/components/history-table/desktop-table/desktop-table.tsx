import type { HistoryRecord } from "~/components/history-table/models/history-record";
import StatusChip from "~/components/status-chip/status-chip";
import { getDocumentType } from "~/components/document-form/document-form.module";
import React from "react";

export default function DesktopTable({
  records,
}: {
  records: HistoryRecord[];
}) {
  return (
    <div className="hidden md:block bg-[#313445] p-6 rounded-xl m-4 shadow-2xl border border-[#3D4052]">
      <div className="flex items-center justify-center mb-4">
        <h2 className="text-lg font-semibold text-[#E6E6E6]">
          Historie kontrol
        </h2>
        <div />
      </div>
      <div className="rounded-lg border border-[#3D4052]">
        <table className="min-w-full text-sm text-lefdt">
          <thead className="bg-[#2A2C39] text-[#B6BAC5]">
            <tr>
              <th className="px-4 py-3 text-left font-medium border-b border-[#3D4052]">
                Dokument
              </th>
              <th className="px-4 py-3 text-left font-medium border-b border-[#3D4052]">
                Výsledek
              </th>
              <th className="px-4 py-3 text-left font-medium border-b border-[#3D4052]">
                Typy
              </th>
              <th className="px-4 py-3 text-left font-medium border-b border-[#3D4052]">
                Zkontrolováno
              </th>
            </tr>
          </thead>
          <tbody className="divide-y divide-[#3D4052]">
            {records.length === 0 ? (
              <tr>
                <td
                  colSpan={4}
                  className="px-4 py-6 text-center text-[#B6BAC5]"
                >
                  Žádné záznamy k zobrazení
                </td>
              </tr>
            ) : (
              records.map((r) => (
                <tr key={r.id} className="hover:bg-[#2B2D3A] transition-colors">
                  <td className="px-4 py-3 text-[#E6E6E6] font-medium">
                    {r.documentNumber}
                  </td>
                  <td className="px-4 py-3">
                    <StatusChip resultType={r.resultType} />
                  </td>
                  <td className="px-4 py-3">
                    <div className="flex flex-wrap gap-2">
                      {r.documentTypes.map((dt) => (
                        <span
                          key={dt}
                          className="px-2 py-1 rounded-md text-xs bg-[#2A2C39] text-[#B6BAC5] border border-[#3D4052]"
                        >
                          {getDocumentType(dt)}
                        </span>
                      ))}
                    </div>
                  </td>
                  <td className="px-4 py-3 text-[#B6BAC5]">
                    {new Date(r.checkedAt).toLocaleString("cs-CZ")}
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
