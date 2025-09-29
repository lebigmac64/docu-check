import StatusChip from "~/components/status-chip/status-chip";
import { getDocumentType } from "~/components/document-form/document-form.module";
import React from "react";
import type { HistoryRecord } from "~/components/history-table/models/history-record";

export default function MobileTable({ records }: { records: HistoryRecord[] }) {
  return (
    <div className="md:hidden space-y-4 mx-3 mt-4">
      {records.length === 0 ? (
        <div className="text-[#B6BAC5] text-sm">Žádné záznamy k zobrazení</div>
      ) : (
        records.map((r) => (
          <div
            key={r.id}
            className="bg-[#2A2C39] border border-[#3D4052] rounded-lg p-4 text-left"
          >
            <div className="text-xs text-[#B6BAC5]">Dokument</div>
            <div className="text-[#E6E6E6] font-medium mb-2">
              {r.documentNumber}
            </div>

            <div className="text-xs text-[#B6BAC5]">Výsledek</div>
            <div className="mb-2">
              <StatusChip resultType={r.resultType} />
            </div>

            <div className="text-xs text-[#B6BAC5]">Typy</div>
            <div className="flex flex-wrap gap-2 mb-2">
              {r.documentTypes.length === 0 ? (
                <span className="text-[#B6BAC5] text-sm">—</span>
              ) : (
                r.documentTypes.map((dt) => (
                  <span
                    key={dt}
                    className="px-2 py-1 rounded-md text-xs bg-[#2A2C39] text-[#B6BAC5] border border-[#3D4052]"
                  >
                    {getDocumentType(dt)}
                  </span>
                ))
              )}
            </div>

            <div className="text-xs text-[#B6BAC5]">Zkontrolováno</div>
            <div className="text-[#B6BAC5] text-sm">
              {new Date(r.checkedAt).toLocaleString("cs-CZ")}
            </div>
          </div>
        ))
      )}
    </div>
  );
}
