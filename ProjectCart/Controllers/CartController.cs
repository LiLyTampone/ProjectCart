using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectCart.Models;

public class CartController : Controller
{
    private const string CartSessionKey = "Cart";

    public IActionResult Index()
    {
        var cart = GetCartFromSession();
        return View(cart);
    }

    public IActionResult AddToCart(int id, string name, string imageUrl, decimal price)
    {
        var cart = GetCartFromSession();
        var existingItem = cart.FirstOrDefault(p => p.ProductId == id);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            cart.Add(new CartItem
            {
                ProductId = id,
                ProductName = name,
                ImageUrl = imageUrl,
                Price = price,
                Quantity = 1
            });
        }

        SaveCartToSession(cart);
        return Json(new { success = true, message = "Sản phẩm đã được thêm vào giỏ!" });
    }

    public IActionResult UpdateCart(int id, int quantity)
    {
        var cart = GetCartFromSession();
        var item = cart.FirstOrDefault(p => p.ProductId == id);

        if (item != null)
        {
            if (quantity > 0)
            {
                item.Quantity = quantity;
            }
            else
            {
                cart.Remove(item);
            }
            SaveCartToSession(cart);
        }

        return Json(new { success = true, message = "Cập nhật giỏ hàng thành công!" });
    }

    public IActionResult RemoveFromCart(int id)
    {
        var cart = GetCartFromSession();
        var item = cart.FirstOrDefault(p => p.ProductId == id);

        if (item != null)
        {
            cart.Remove(item);
            SaveCartToSession(cart);
        }

        return Json(new { success = true, message = "Đã xóa sản phẩm khỏi giỏ hàng!" });
    }

    private List<CartItem> GetCartFromSession()
    {
        var cartJson = HttpContext.Session.GetString(CartSessionKey);
        return string.IsNullOrEmpty(cartJson) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
    }

    private void SaveCartToSession(List<CartItem> cart)
    {
        HttpContext.Session.SetString(CartSessionKey, JsonConvert.SerializeObject(cart));
    }
}
