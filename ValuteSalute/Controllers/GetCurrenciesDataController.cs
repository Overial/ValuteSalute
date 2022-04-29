using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ValuteSalute.Domain.Entities;
using ValuteSalute.Models;
using ValuteSalute.Service;

namespace ValuteSalute.Controllers
{
    public class GetCurrenciesDataController : Controller
    {
        static readonly HttpClient _httpClient = new HttpClient();
        static CurrencyItemsCacherModel CurrencyItemsCacherModel = new CurrencyItemsCacherModel();

        public GetCurrenciesDataController() { }

        [HttpGet("currencies")]
        public async Task<IActionResult> GetCurrenciesList
        (
            string sortCol,
            string sortParam,
            string pagingSortParam,
            int pageNumber = 1
        )
        {
            string content = "";

            bool bIsHttpRequestFailed = false;
            bool bIsJsonDeserializingFailed = false;
            bool bIsJsonParsingFailed = false;

            Dictionary<string, CurrencyItem>? currencyItemsDict = null;

            if (GetCurrenciesDataController.CurrencyItemsCacherModel.CurrencyItemsCache == null ||
                GetCurrenciesDataController.CurrencyItemsCacherModel.TTL == 0)
			{
                // Get latest currency data
                var response = await _httpClient.GetAsync(@"https://www.cbr-xml-daily.ru/daily_json.js");
                try
                {
                    response.EnsureSuccessStatusCode();
                    content = await response.Content.ReadAsStringAsync();
                }
                catch (Exception)
                {
                    bIsHttpRequestFailed = true;
                }

                // Get currency data from JSON
                try
                {
                    // Deserialize JSON data
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

                    // Parse JSON data
                    foreach (var item in dict)
                    {
                        try
                        {
                            // Get valute data
                            if (item.Key == "Valute")
                            {
                                var newDict = item.Value.ToString();
                                currencyItemsDict = JsonConvert.DeserializeObject<Dictionary<string, CurrencyItem>>(newDict);

                                // Cash valute data
                                GetCurrenciesDataController.CurrencyItemsCacherModel.CurrencyItemsCache = currencyItemsDict;
                            }
                        }
                        catch (Exception)
                        {
                            bIsJsonParsingFailed = true;
                        }
                    }
                }
                catch (Exception)
                {
                    bIsJsonDeserializingFailed = true;
                }
            }
			else
			{
                currencyItemsDict = GetCurrenciesDataController.CurrencyItemsCacherModel.CurrencyItemsCache;
                --GetCurrenciesDataController.CurrencyItemsCacherModel.TTL;
            }

            // Send error info to view
            ViewBag.IsHttpRequestFailed = bIsHttpRequestFailed;
            ViewBag.IsJsonDeserializingFailed = bIsJsonDeserializingFailed;
            ViewBag.IsJsonParsingFailed = bIsJsonParsingFailed;

            // Sorting data
            IOrderedEnumerable<KeyValuePair<string, CurrencyItem>>? sortedСurrencyData = null;
            if (sortCol != null)
			{
                ViewBag.SortCol = sortCol;
                if (pagingSortParam != null)
				{
                    ViewBag.SortParam = pagingSortParam;
                }
				else
				{
                    ViewBag.SortParam = sortParam == "Desc" ? "Asc" : "Desc";
                }
                var tempSortParam = ViewBag.SortParam;

                switch (sortCol)
                {
                case "CurrencyId":
                    switch (tempSortParam)
                    {
                    case "Asc":
                        sortedСurrencyData = currencyItemsDict.OrderBy(c => c.Value.CurrencyId);
                        break;
                    case "Desc":
                        sortedСurrencyData = currencyItemsDict.OrderByDescending(w => w.Value.CurrencyId);
                        break;
                    default:
                        break;
                    }
                    break;
                case "NumCode":
                    switch (tempSortParam)
                    {
                    case "Asc":
                        sortedСurrencyData = currencyItemsDict.OrderBy(c => c.Value.NumCode);
                        break;
                    case "Desc":
                        sortedСurrencyData = currencyItemsDict.OrderByDescending(w => w.Value.NumCode);
                        break;
                    default:
                        break;
                    }
                    break;
                case "CharCode":
                    switch (tempSortParam)
                    {
                    case "Asc":
                        sortedСurrencyData = currencyItemsDict.OrderBy(c => c.Value.CharCode);
                        break;
                    case "Desc":
                        sortedСurrencyData = currencyItemsDict.OrderByDescending(w => w.Value.CharCode);
                        break;
                    default:
                        break;
                    }
                    break;
                case "Nominal":
                    switch (tempSortParam)
                    {
                    case "Asc":
                        sortedСurrencyData = currencyItemsDict.OrderBy(c => c.Value.Nominal);
                        break;
                    case "Desc":
                        sortedСurrencyData = currencyItemsDict.OrderByDescending(w => w.Value.Nominal);
                        break;
                    default:
                        break;
                    }
                    break;
                case "Name":
                    switch (tempSortParam)
                    {
                    case "Asc":
                        sortedСurrencyData = currencyItemsDict.OrderBy(c => c.Value.Name);
                        break;
                    case "Desc":
                        sortedСurrencyData = currencyItemsDict.OrderByDescending(w => w.Value.Name);
                        break;
                    default:
                        break;
                    }
                    break;
                case "Value":
                    switch (tempSortParam)
                    {
                    case "Asc":
                        sortedСurrencyData = currencyItemsDict.OrderBy(c => c.Value.Value);
                        break;
                    case "Desc":
                        sortedСurrencyData = currencyItemsDict.OrderByDescending(w => w.Value.Value);
                        break;
                    default:
                        break;
                    }
                    break;
                case "Previous":
                    switch (tempSortParam)
                    {
                    case "Asc":
                        sortedСurrencyData = currencyItemsDict.OrderBy(c => c.Value.Previous);
                        break;
                    case "Desc":
                        sortedСurrencyData = currencyItemsDict.OrderByDescending(w => w.Value.Previous);
                        break;
                    default:
                        break;
                    }
                    break;
                default:
                    break;
                }
            }

            // Set pagination options
            int pageSize = 7;
            int count = 0;
            IEnumerable<KeyValuePair<string, CurrencyItem>>? finalCurrencyData = null;
            if (sortedСurrencyData != null)
			{
                count = sortedСurrencyData.Count();
                finalCurrencyData = sortedСurrencyData.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                ViewBag.PageNumber = pageNumber;
            }
			else
			{
                count = currencyItemsDict.Count();
                finalCurrencyData = currencyItemsDict.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                ViewBag.PageNumber = pageNumber;
            }

            // Model for pagination
            PageViewModel pageViewModel = new PageViewModel(count, pageNumber, pageSize);

            // Get currency id info for drop down list
            var currencyIdSelectList = new SelectList(currencyItemsDict.Select(c => c.Value.CurrencyId).Distinct());

            // Create currency view model for rendering data
            CurrencyViewModel currencyViewModel = new CurrencyViewModel()
            {
                CurrencyData = finalCurrencyData,
                PageViewModel = pageViewModel,
                CurrencyIdSelectList = currencyIdSelectList
            };

            return View("GetCurrenciesList", currencyViewModel);
        }

        [HttpGet("currency/{currencyIdSearchString?}")]
        public async Task<IActionResult> GetCurrencyByCurrencyId
        (
            string ID,
            int pageNumber = 1
        )
        {
            if (ID == null)
			{
                RedirectToAction(nameof(GetCurrenciesDataController.GetCurrenciesList), nameof(GetCurrenciesDataController).CutController());
			}

            string content = "";

            bool bIsHttpRequestFailed = false;
            bool bIsJsonDeserializingFailed = false;
            bool bIsJsonParsingFailed = false;

            Dictionary<string, CurrencyItem>? currencyItemsDict = null;

            if (GetCurrenciesDataController.CurrencyItemsCacherModel.CurrencyItemsCache == null ||
                GetCurrenciesDataController.CurrencyItemsCacherModel.TTL == 0)
            {
                // Get latest currency data
                var response = await _httpClient.GetAsync(@"https://www.cbr-xml-daily.ru/daily_json.js");
                try
                {
                    response.EnsureSuccessStatusCode();
                    content = await response.Content.ReadAsStringAsync();
                }
                catch (Exception)
                {
                    bIsHttpRequestFailed = true;
                }

                // Get currency data from JSON
                try
                {
                    // Deserialize JSON data
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

                    // Parse JSON data
                    foreach (var item in dict)
                    {
                        try
                        {
                            // Get valute data
                            if (item.Key == "Valute")
                            {
                                var newDict = item.Value.ToString();
                                currencyItemsDict = JsonConvert.DeserializeObject<Dictionary<string, CurrencyItem>>(newDict);

                                // Cash valute data
                                GetCurrenciesDataController.CurrencyItemsCacherModel.CurrencyItemsCache = currencyItemsDict;
                            }
                        }
                        catch (Exception)
                        {
                            bIsJsonParsingFailed = true;
                        }
                    }
                }
                catch (Exception)
                {
                    bIsJsonDeserializingFailed = true;
                }
            }
            else
            {
                currencyItemsDict = GetCurrenciesDataController.CurrencyItemsCacherModel.CurrencyItemsCache;
                --GetCurrenciesDataController.CurrencyItemsCacherModel.TTL;
            }

            // Send error info to view
            ViewBag.IsHttpRequestFailed = bIsHttpRequestFailed;
            ViewBag.IsJsonDeserializingFailed = bIsJsonDeserializingFailed;
            ViewBag.IsJsonParsingFailed = bIsJsonParsingFailed;

            // Search for desired currency
            ViewBag.CurrencyIdSearchString = ID;
            var searchedCurrency = currencyItemsDict.Where(c => c.Value.CurrencyId.Contains(ID));

            // Get currency id info for drop down list
            var currencyIdSelectList = new SelectList(currencyItemsDict.Select(c => c.Value.CurrencyId).Distinct());

            CurrencyViewModel currencyViewModel = new CurrencyViewModel()
            {
                CurrencyData = searchedCurrency,
                PageViewModel = null,
                CurrencyIdSelectList = currencyIdSelectList
            };

            return View("GetCurrenciesList", currencyViewModel);
        }

        public IActionResult Reset()
        {
            ViewData.Clear();

            return RedirectToAction(nameof(GetCurrenciesDataController.GetCurrenciesList), nameof(GetCurrenciesDataController).CutController());
        }
    }
}
