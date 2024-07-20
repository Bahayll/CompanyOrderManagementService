# Firma Sipariş Yönetim Servisi

## Tablo Açıklamaları
* Firma: Adı, Onay Durumu, Sipariş İzin başlangıç saati, Sipariş İzin bitiş saati
* Ürünler: Firma, Adı, Stok, Fiyatı
* Sipariş: Id, Firma, Ürün, Siparişi veren kişinin adı, Sipariş Tarihi
  
## Servisler
1. Firma Ekleme
   * Tüm bilgiler eklenebilir.
2. Firma Güncelleme
   * Sipariş İzin Saati Güncelleme
   * Onay Durumu Güncelleme
3. Firma Listeleme
   * Tüm firmalar ve sutünları getirilir.
4. Firma Silme

5. Ürün Ekleme
6. Ürün Güncelleme
7. Ürün Listeleme
8. Ürün Silme
    
9. Sipariş Oluşturma
   *İlgili Firma onaylı ise sipariş oluşturulabilir.
   *Sipariş oluştur dediğimizdeki saat, firma izin saatleri arasında ise sipariş oluşturulabilir.
    * Test edilebilmesi için Şuanki saati istek içerisinde gönderebilirsiniz.
    * Örneğin Firma Sipariş İzin Saatleri: 08:30-11:00 olsun
         i. Saat 08:29’da sipariş alınamaz
         ii. Saat 08:30’da sipariş alınabilir
         iii. Saat 09:55’te sipariş alınabilir
         iv. Saat 11:00’da sipariş alınabilir
          v. Saat 16:47’de sipariş alınamaz
10. Sipariş Güncelleme
11. Sipariş Listeleme
12. Sipariş Silme

## Kullanılan Teknolojiler
.NET Core 6 API, MSSQL, SwaggerUI, Entity Framework Code First, Repository Design Pattern
