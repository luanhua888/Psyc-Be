using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsychologicalCounseling.Models;
using ZXing;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

namespace PsychologicalCounseling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositsController : ControllerBase
    {
        private readonly PsychologicalCouselingContext _context;

        public DepositsController(PsychologicalCouselingContext context)
        {
            _context = context;
        }

        // GET: api/Deposits


        [HttpGet("GenerateQRCode")]
        public async Task<ActionResult> GetDeposits(string sdt, string name, string amount)
        {          
            var qrcode_text = $"2|99|{sdt}|{name}|ttrungta2031@gmail.com|0|0|{amount}";
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            EncodingOptions enconding = new EncodingOptions() { Width = 300, Height = 300, Margin = 0, PureBarcode = false };
            enconding.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            barcodeWriter.Renderer = new BitmapRenderer();
            barcodeWriter.Options = enconding;
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var bitmap = barcodeWriter.Write(qrcode_text);
            // Bitmap logo = "https://firebasestorage.googleapis.com/v0/b/psychologicalcounseling-28efa.appspot.com/o/images%2FMoMo_Logo.png?alt=media&token=65c3a8b0-9324-475b-875a-4f1bd6bd290a";
            // Graphics g = Graphics.FromImage(bitmap);

            //  g.DrawImage( new Point((bitmap.Width), (bitmap.Height)));
            // bitmap.Save(path,ImageFormat.Jpeg);

            var bytes = ImagetoByteArrayaa(bitmap);

            return File(bytes, "image/bmp");
            

            return Ok(new { StatusCode = 200, Content = "qrcode generate successful", data = bytes });
        }

        // GET: api/Deposits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Deposit>> GetDeposit(int id)
        {
            var deposit = await _context.Deposits.FindAsync(id);

            if (deposit == null)
            {
                return NotFound();
            }

            return deposit;
        }

        // PUT: api/Deposits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeposit(int id, Deposit deposit)
        {
            if (id != deposit.Id)
            {
                return BadRequest();
            }

            _context.Entry(deposit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepositExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Deposits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Deposit>> PostDeposit(Deposit deposit)
        {
            _context.Deposits.Add(deposit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeposit", new { id = deposit.Id }, deposit);
        }

        // DELETE: api/Deposits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeposit(int id)
        {
            var deposit = await _context.Deposits.FindAsync(id);
            if (deposit == null)
            {
                return NotFound();
            }

            _context.Deposits.Remove(deposit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepositExists(int id)
        {
            return _context.Deposits.Any(e => e.Id == id);
        }
        private  byte[] ImagetoByteArrayaa(System.Drawing.Image imagein)
        {
            MemoryStream ms = new MemoryStream();
            imagein.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}
