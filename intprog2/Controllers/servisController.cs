using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using intprog2.ViewModel;
using intprog2.Models;

namespace intprog2.Controllers
{
    public class servisController : ApiController

    {
        DB01Entities3 db = new DB01Entities3();
        sonucModel sonuc = new sonucModel();

        #region soru


        [HttpGet]
        [Route("ApiController/SoruListe")]
        public List<soruModel> SoruListe()
        {
            List<soruModel> liste = db.soru.Select(x => new soruModel()
            {
                soruNo = x.soruNo,
                yanıtNo = x.yanıtNo


            }).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/soruId/{soruNo}")]
        public soruModel SoruById(string soruId)
        {

            soruModel kayit = db.soru.Where(s => s.soruNo == soruId).Select(x => new soruModel()
            {
                soruNo = x.soruNo,
                yanıtNo = x.yanıtNo
            }).SingleOrDefault();
            return kayit;
        }


        [HttpPost]
        [Route("api/sorukle")]
        public sonucModel SoruEkle(soruModel model)
        {
            if (db.soru.Count(c => c.soruNo == model.soruNo) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Ders Kodu Kayıtlıdır!";
                return sonuc;
            }

            soru yeni = new soru();
            yeni.soruNo = Guid.NewGuid().ToString();
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ders Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/soruduzenle")]
        public sonucModel SoruDuzenle(soruModel model)
        {

            soru kayit = db.soru.Where(s => s.soruNo == model.soruNo).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }


            kayit.soruNo = model.soruNo;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ders Düzenlendi";

            return sonuc;
        }



        #endregion
        #region Öğrenci


        [HttpGet]
        [Route("api/ogrenciliste")]
        public List<ogrModel> OgrenciListe()
        {
            List<ogrModel> liste = db.ogr.Select(x => new ogrModel()
            {
                ogrId = x.ogrId,
                ogrNo = x.ogrNo,
                ogrAdsoyad = x.ogrAdsoyad,
                ogrMail = x.ogrMail,
                ogrSoru = x.ogrSoru,

            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/ogrencibyid/{ogrId}")]
        public ogrModel OgrenciById(string ogrId)
        {

            ogrModel kayit = db.ogr.Where(s => s.ogrNo == ogrId).Select(x => new ogrModel()
            {
                ogrId = x.ogrId,
                ogrNo = x.ogrNo,
                ogrAdsoyad = x.ogrAdsoyad,
                ogrMail = x.ogrMail,
                ogrSoru = x.ogrSoru,
            }).SingleOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/ogrenciekle")]
        public sonucModel OgrenciEkle(ogrModel model)
        {
            if (db.ogr.Count(c => c.ogrNo == model.ogrNo) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Öğrenci Numarası Kayıtlıdır!";
                return sonuc;
            }

            ogr yeni = new ogr();
            yeni.ogrNo = Guid.NewGuid().ToString();
            yeni.ogrId = model.ogrId;
            yeni.ogrAdsoyad = model.ogrAdsoyad;
            yeni.ogrMail = model.ogrMail;
             db.ogr.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/ogrenciduzenle")]
        public sonucModel OgrenciDuzenle(ogrModel model)
        {

            ogr kayit = db.ogr.Where(s => s.ogrId == model.ogrId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }


            kayit.ogrNo = model.ogrNo;
            kayit.ogrAdsoyad = model.ogrAdsoyad;
          

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Düzenlendi";

            return sonuc;
        }
        [HttpDelete]
        [Route("api/ogrencisil/{ogrId}")]
        public sonucModel OgrenciSil(string ogrId)
        {

            ogr kayit = db.ogr.Where(s => s.ogrNo == ogrId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            if (db.ogr.Count(c => c.ogrNo == ogrId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Sorusu Olan Öğrenci Silinemez!";
                return sonuc;
            }


            db.ogr.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Silindi";

            return sonuc;
        }

        #endregion
        #region Öğretmen
        [HttpGet]
        [Route("api/ogretmenliste")]
        public List<ogrtModel> OgretmenListe()
        {
            List<ogrtModel> liste = db.ogrt.Select(x => new ogrtModel()
            {
                ogrtId = x.ogrtId,
                ogrtNo = x.ogrtNo,
                ogrtAdsoyad = x.ogrtAdsoyad,
                ogrtMail = x.ogrtMail,
                ogrtYanıt = x.ogrtYanıt,

            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/ogretmenbyid/{ogrId}")]
        public ogrtModel OgretmenById(string ogrtId)
        {

            ogrtModel kayit = db.ogrt.Where(s => s.ogrtNo == ogrtId).Select(x => new ogrtModel()
            {
                ogrtId = x.ogrtId,
                ogrtNo = x.ogrtNo,
                ogrtAdsoyad = x.ogrtAdsoyad,
                ogrtMail = x.ogrtMail,
                ogrtYanıt = x.ogrtYanıt,
            }).SingleOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/ogretmenekle")]
        public sonucModel OgretmenEkle(ogrtModel model)
        {
            if (db.ogrt.Count(c => c.ogrtNo == model.ogrtNo) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Öğretmen Numarası Kayıtlıdır!";
                return sonuc;
            }

            ogrt yeni = new ogrt();
            yeni.ogrtNo = Guid.NewGuid().ToString();
            yeni.ogrtId = model.ogrtId;
            yeni.ogrtAdsoyad = model.ogrtAdsoyad;
            yeni.ogrtMail = model.ogrtMail;
            db.ogrt.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğretmen Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/ogretmenduzenle")]
        public sonucModel OgretmenDuzenle(ogrtModel model)
        {

            ogrt kayit = db.ogrt.Where(s => s.ogrtId == model.ogrtId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }


            kayit.ogrtNo = model.ogrtNo;
            kayit.ogrtAdsoyad = model.ogrtAdsoyad;


            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğretmen Düzenlendi";

            return sonuc;
        }
        [HttpDelete]
        [Route("api/ogrencisil/{ogrId}")]
        public sonucModel OgretmenSil(string ogrtId)
        {

            ogrt kayit = db.ogrt.Where(s => s.ogrtNo == ogrtId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            if (db.ogrt.Count(c => c.ogrtNo == ogrtId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Yanıtı olan öğretmen Silinemez!";
                return sonuc;
            }


            db.ogrt.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Silindi";

            return sonuc;
        }
        #endregion
    }

}




