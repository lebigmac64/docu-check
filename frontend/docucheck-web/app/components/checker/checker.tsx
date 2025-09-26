export default function Checker({...props}: React.HTMLAttributes<HTMLDivElement>) {
  return (
    <div {...props} >
      <h1>Kontrola platnosti dokumentu</h1>
      <p id="document-check-instructions">Vložte číslo dokumentu k ověření platnosti.</p>
      <div className="input-group">
      <label htmlFor="document-number">Číslo dokumentu</label>
      <input
        id="document-number"
        type="text"
        placeholder="Zadejte číslo dokumentu"
        aria-describedby="document-check-instructions"
      />
      </div>
      <progress value="33" max="100">1 ze 3 typů dokumentu</progress>
      <button type="submit">Ověřit</button>
    </div>
  );
}
