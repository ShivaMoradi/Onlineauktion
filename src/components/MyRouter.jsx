
import { BrowserRouter, Routes, Route } from "react-router-dom"
import MyNav from "./MyNav.jsx"
import HomePage from "../pages/HomePage.jsx"
import MySearchInputPage from "../pages/MySearchInputPage.jsx"
import ContactPage from "../pages/ContactPage.jsx"
import { GlobalProvider } from './GlobalContext.jsx'
import Cart from '../pages/Cart.jsx'
import ShowAuctionPage from "../pages/ShowAuctionPage.jsx"
import NewAuctionPage from "../pages/NewAuctionPage.jsx"


function MyRouter() {

  return (
    <GlobalProvider>
      <BrowserRouter>
        <div className="page corners padding transp">
          <MyNav />

          <main>
            <div>
              <Routes>
                <Route path="/" element={<HomePage />}></Route>
                <Route path="/my-search-input-page" element={<MySearchInputPage />} />
                <Route path="/contact-page" element={<ContactPage />} />
                <Route path="/cart-page" element={<Cart />} />
                <Route path="/show-auction-page" element={<ShowAuctionPage />} />
                <Route path="/new-auction-page" element={<NewAuctionPage />} />
              </Routes>
            </div>
          </main>
        </div>
      </BrowserRouter>
    </GlobalProvider>
  )

}

export default MyRouter
