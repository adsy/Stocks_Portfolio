const CgtSalesList = ({ salesList }) => {
  console.log(salesList);
  return (
    <div className="cgt-sales-list">
      {salesList.map((item, key) => {
        return (
          <div className="cgt-sold-row" key={key}>
            <div className="full-row">
              <h4>Name: {item.name}</h4>
            </div>
            <div className="full-row">Amount: {item.amount}</div>
            <div className="full-row">
              <h6 className="h6-margin-fix">
                <span className={item.profit > 0 ? "green-text" : "red-text"}>
                  Profit: ${item.profit.toFixed(3)}
                </span>
              </h6>
            </div>
            <div className="full-row">
              Discount Applied: {item.discountApplied ? "True" : "False"}
            </div>
            <div className="full-row">
              CGT Payable: ${item.cgtPayable.toFixed(3)}
            </div>
          </div>
        );
      })}
    </div>
  );
};

export default CgtSalesList;
