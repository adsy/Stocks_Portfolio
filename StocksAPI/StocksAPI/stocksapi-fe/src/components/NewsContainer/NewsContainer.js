import React, { useEffect } from "react";
import { Row } from "react-bootstrap";
import { Link } from "react-router-dom";

const NewsContainer = ({ stockNewsData }) => {
  useEffect(() => {
    console.log(stockNewsData);
  }, []);

  return (
    <div style={{ display: "flex", flexDirection: "column" }}>
      {stockNewsData.map((stockNews) => (
        <div
          className="cool-shadow"
          style={{
            marginBottom: "40px",
            padding: "10px",
            border: "1px black dotted",
            fontSize: "13px",
          }}
        >
          <h5>{stockNews.title}</h5>
          <h6>{stockNews.summary}</h6>
          <div>{stockNews.publisher}</div>
          <div>{stockNews.type}</div>
          <div>{stockNews.publishedAt}</div>
          <Link href={stockNews.link}>{stockNews.link}</Link>
        </div>
      ))}
    </div>
  );
};

export default NewsContainer;
